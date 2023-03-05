using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Enemy
{
    [Serializable]
    public struct AIContext
    {
        public Rigidbody2D rigidbody2D;
        public Path        path;
        public Transform   target;
        public Seeker      seeker;
        public LayerMask   playerLayerMask;
        public int         currentWaypoint;
        public int         nextWaypointDistance;
        public float       speed;
        public float       maxRandomPathDistance;
        public float       searchRange;
        public float       timeToNextSearchPath;
        public float       memoryTime;
        public string      AIType;
        public bool        reachedEndOfPath;
    }
    public abstract class AIType
    {
        public abstract string    Name { get; }
        public abstract AIContext CTX  { get; set; }
        public abstract void      Start();
        public abstract void      Process();
        public abstract void      Reapeat();
    }

    public class DebugSeeker : AIType
    {
        public override string    Name => "DebugSeeker";
        public override AIContext CTX
        {
            get => ctx;
            set => ctx = value;
        }
        private         AIContext ctx;
        

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Process()
        {
            Debug.Log("Debug Enemy AI Processing");
        }

        public override void Reapeat()
        {
            return;
        }

        public override void Start()
        {
            Debug.Log("Debug Enemy AI Start");
        }
    }

    public class StandardChaser : AIType
    {
        public override string Name => "StandardChaser";
        public override AIContext CTX
        {
            get => ctx;
            set => ctx = value;
        }
        private AIContext ctx;

        public override void Start()
        {
            return;
        }
        
        

        public override void Process()
        {
            if(ctx.path == null) return;

            if (ctx.currentWaypoint >= ctx.path.vectorPath.Count)
            {
                ctx.reachedEndOfPath = true;
                return;
            }
            else
            {
                ctx.reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)ctx.path.vectorPath[ctx.currentWaypoint] - ctx.rigidbody2D.position)
                .normalized;
            Vector2 force = direction * (ctx.speed * Time.deltaTime);

            ctx.rigidbody2D.AddForce(force);
            
            float distance = Vector2.Distance(ctx.rigidbody2D.position, ctx.path.vectorPath[ctx.currentWaypoint]);

            if (distance < ctx.nextWaypointDistance)
            {
                ctx.currentWaypoint++;
            }
        }

        public override void Reapeat()
        {
            UpdatePath();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePath()
        {
            if (ctx.seeker.IsDone())
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, OnPathCompleted);
        }

        private void OnPathCompleted(Path newPath)
        {
            if (!newPath.error)
            {
                ctx.path            = newPath;
                ctx.currentWaypoint = 0;
            }
        }
    }

    public class StandardSeeker : AIType
    {
        public override string Name => "StandardSeeker";
        public override AIContext CTX
        {
            get => ctx;
            set => ctx = value;
        }
        private AIContext ctx;
        private float     waitTime = 0;
        private float     memoTime = 0;

        public override void      Start()
        {
            
        }

        public override void      Process()
        {
            if(ctx.path == null || ctx.path.vectorPath == null) return;

            if (ctx.currentWaypoint >= ctx.path.vectorPath.Count)
            {
                ctx.reachedEndOfPath = true;
                return;
            }
            else
            {
                ctx.reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)ctx.path.vectorPath[ctx.currentWaypoint] - ctx.rigidbody2D.position)
                .normalized;
            Vector2 force = direction * (ctx.speed * Time.deltaTime);

            ctx.rigidbody2D.AddForce(force);
            
            float distance = Vector2.Distance(ctx.rigidbody2D.position, ctx.path.vectorPath[ctx.currentWaypoint]);

            if (distance < ctx.nextWaypointDistance)
            {
                ctx.currentWaypoint++;
            }
        }

        public override void      Reapeat()
        {
            UpdatePath();
        }
        
        public void UpdatePath()
        {
            if(!ctx.seeker.IsDone()) return;
            if (ctx.target != null)
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, OnPathCompleted);
            else if(waitTime > ctx.timeToNextSearchPath)
            {
                waitTime = 0;
                var position = DesignateNearTransformAtRandom();
                ctx.seeker.StartPath(ctx.rigidbody2D.position, position, OnPathCompleted);  
            }

            waitTime++;
            SearchIfTargetIsInRange();
        }

        public void SearchIfTargetIsInRange()
        {
            var hits = Physics2D.CircleCastAll(ctx.rigidbody2D.position, ctx.searchRange, Vector2.zero, ctx.playerLayerMask.value);

            var hit = hits.FirstOrDefault(x => x.collider.CompareTag("Player") || x.collider.CompareTag("NPC"));
            if(hit.transform != null)
            {
                ctx.target = hit.transform;
                memoTime   = 0;
            }
            else if (memoTime >= ctx.memoryTime)
            {
                memoTime   = 0;
                ctx.target = null;
                ctx.seeker.CancelCurrentPathRequest();
            }
            else
            {
                memoTime++;
            }
        }

        public Vector3 DesignateNearTransformAtRandom()
        {
            var newPosition = ctx.rigidbody2D.position;
            newPosition.x = Random.Range(newPosition.x - ctx.maxRandomPathDistance /2, newPosition.y + ctx.maxRandomPathDistance/2);
            newPosition.y = Random.Range(newPosition.y - ctx.maxRandomPathDistance /2, newPosition.y + ctx.maxRandomPathDistance/2);
            return newPosition;
        }

        private void OnPathCompleted(Path newPath)
        {
            if (!newPath.error)
            {
                ctx.path            = newPath;
                ctx.currentWaypoint = 0;
            }
        }

    }

    public static class EnemyAIFactory
    {
        private static Dictionary<string, Type> _aiTypes;
        private static bool _isInitialized = false;

        
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
                throw new Exception();
            }
        }
        
        
    }

}
