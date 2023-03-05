using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Code.Utilities;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Timer = Code.Utilities.Timer;

namespace Code.Enemy
{
    [Serializable]
    public struct AIContext
    {
        public  Rigidbody2D rigidbody2D;
        public  Path        path;
        public  Transform   target;
        public  Seeker      seeker;
        public  AIData      data;
        internal bool        reachedEndOfPath;        
        internal int currentWaypoint;
    }
    public abstract class AIType
    {
        public abstract string    Name { get; }
        /// <summary>
        /// Shared context of AI data
        /// </summary>
        public virtual AIContext CTX {
            get => ctx;
            set => ctx = value;
        }
        protected  AIContext ctx;

        /// <summary>
        /// Launches when the entity call own start method
        /// </summary>
        public virtual void Start()
        {
            return;
        }

        /// <summary>
        /// Every physics frame update, used for calculate AI movement logic
        /// </summary>
        public virtual void Process() {
            if(ctx.path == null) return;

            if(IsPathFinished()) return;

            Vector2 force = CalculateDirectionAndForce();

            ctx.rigidbody2D.AddForce(force);
            
            HandleWaypointReach();
        }

        /// <summary>
        /// Determines when the path is completed
        /// </summary>
        /// <returns>true if path is completed, false in every other situation</returns>
        private bool IsPathFinished()
        {
            if (ctx.currentWaypoint >= ctx.path.vectorPath.Count)
            {
                return ctx.reachedEndOfPath = true;
            }
            else
            {
                return ctx.reachedEndOfPath = false;
            }
        }
        /// <summary>
        /// Handles changing waypoints to new when current is reached
        /// </summary>
        private void HandleWaypointReach()
        {
            float distance = Vector2.Distance(ctx.rigidbody2D.position, ctx.path.vectorPath[ctx.currentWaypoint]);

            if (distance < ctx.data.nextWaypointDistance)
            {
                ctx.currentWaypoint++;
            }
        }
        /// <summary>
        /// Calculates Direction by substracting current position from waypoint position and normalizing, then uses it to calculate force vector
        /// </summary>
        /// <returns>Vector2 representing force in direction of waypoint</returns>
        private Vector2 CalculateDirectionAndForce()
        {
            Vector2 direction = ((Vector2)ctx.path.vectorPath[ctx.currentWaypoint] - ctx.rigidbody2D.position)
                .normalized;
            return direction * (ctx.data.speed * Time.deltaTime);
        }
        /// <summary>
        /// Callback function that handles path changing
        /// </summary>
        /// <param name="newPath"></param>
        protected virtual void OnPathCompleted(Path newPath)
        {
            if (!newPath.error)
            {
                ctx.path            = newPath;
                ctx.currentWaypoint = 0;
            }
        }

        /// <summary>
        /// exposes access to InvokeRepeating in base entity
        /// </summary>
        public virtual void Reapeat()
        {
            return;
        }
    }

    public sealed class DebugSeeker : AIType
    {
        public override string    Name => "DebugSeeker";
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Process()
        {
            Debug.Log("Debug Enemy AI Processing");
        }

        public override void Start()
        {
            Debug.Log("Debug Enemy AI Start");
        }
    }

    public class StandardChaser : AIType
    {
        public override string Name => "StandardChaser";
        public sealed override void Reapeat()
        {
            UpdatePath();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePath()
        {
            if (ctx.seeker.IsDone())
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, base.OnPathCompleted);
        }

        
    }

    public class StandardSeeker : AIType
    {
        public override string Name => "StandardSeeker";
        private         Timer  waitForNewPathTimer;
        private         Timer  lastSeenTargetTimer;

        public sealed override void Start()
        {
            waitForNewPathTimer = new Timer(ctx.data.timeToNextSearchPath, StartNewRandomPath);
            lastSeenTargetTimer = new Timer(ctx.data.memoryTime,           ResetPath);
        }

        public sealed override void Reapeat()
        {
            UpdatePath();
        }
        
        public void UpdatePath()
        {
            if(!ctx.seeker.IsDone()) return;
            if (ctx.target != null)
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, OnPathCompleted);
            else
            {
                waitForNewPathTimer.Update(1f);
            }
            SearchIfTargetIsInVisibleRange();
        }

        private void StartNewRandomPath()
        {
            var position = DesignateNearTransformAtRandom();
            ctx.seeker.StartPath(ctx.rigidbody2D.position, position, OnPathCompleted); 
        }
        
        /// <summary>
        /// Search area to found if any mob is in visible range and handles the memorization of last target
        /// </summary>
        private void SearchIfTargetIsInVisibleRange()
        {
            var hits = Physics2D.CircleCastAll(ctx.rigidbody2D.position, ctx.data.searchRange, Vector2.zero, ctx.data.playerLayerMask.value);

            var hit = hits.FirstOrDefault(x => x.collider.CompareTag("Player") || x.collider.CompareTag("NPC"));
            if(hit.transform != null)
            {
                ctx.target = hit.transform;
                lastSeenTargetTimer.Reset();
            }
            else
            {
                lastSeenTargetTimer.Update(1f);
            }
        }

        private void ResetPath()
        {
            ctx.target = null;
            ctx.seeker.CancelCurrentPathRequest();
        }
        /// <summary>
        /// Calculate random position near entity position
        /// </summary>
        /// <returns>Vector3 that represents new position</returns>
        private Vector3 DesignateNearTransformAtRandom()
        {
            var newPosition = ctx.rigidbody2D.position;
            newPosition.x = GetRandomFloatWithOffset(newPosition.x, ctx.data.maxRandomPathDistance);
            newPosition.y = GetRandomFloatWithOffset(newPosition.y, ctx.data.maxRandomPathDistance);
            return newPosition;
        }
        /// <summary>
        /// Gets random value between start - offset / 2 and start + offset / 2
        /// </summary>
        /// <param name="start">float that represents start value</param>
        /// <param name="offset">float that represents value that will be substracted/added to start</param>
        /// <returns></returns>
        private float GetRandomFloatWithOffset(float start, float offset)
        {
            return Random.Range(start - offset / 2, start + offset / 2);
        }
    }

    public class StandardRangeSeeker : StandardSeeker
    {
        public override string Name => "StandardRangeSeeker";
    }

    public static class EnemyAIFactory
    {
        private static Dictionary<string, Type> _aiTypes;
        private static bool _isInitialized = false;

        /// <summary>
        /// Gathers all assemblies of enemy AI
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void Initialize()
        {
            if(_isInitialized) return;
            
            var types = Assembly.GetAssembly(typeof(AIType)).GetTypes()
                                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(AIType)));

            _aiTypes = new Dictionary<string, Type>();

            foreach (var type in types)
            {
                var temp = Activator.CreateInstance(type) as AIType;
                if (temp == null) throw new Exception();
                _aiTypes.Add(temp.Name,type);
            }

            _isInitialized = true;
        }
        
        /// <summary>
        /// Returns AI instance of given type
        /// </summary>
        /// <param name="aiTypeName">String representing AI which function should return</param>
        /// <returns></returns>
        /// <exception cref="IncorrectAIException">thrown when AI dont exists</exception>
        public static AIType GetAI(string aiTypeName)
        {
            Initialize();
            if (_aiTypes.ContainsKey(aiTypeName))
            {
                Type type = _aiTypes[aiTypeName];
                var  ai   = Activator.CreateInstance(type) as AIType;
                return ai;
            }
            else
            {
                throw new IncorrectAIException();
            }
        }

        
    }
    
    /// <summary>
    /// Error thrown if factory don't have type of AIType which we want
    /// </summary>
    internal class IncorrectAIException : CodeExecption
    {
        public IncorrectAIException() : base(10001)
        {
        }
    }

}
