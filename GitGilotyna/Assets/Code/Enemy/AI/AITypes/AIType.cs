using System.Linq;
using Code.Player;
using Code.Utilities;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Code.Utilities.Timer;
//TODO Dodać resetowanie ścieżki jeśli pozycja się nie zmienia
namespace Code.Enemy.AITypes
{
    public abstract class AIType : IFactoryData
    {
        public abstract string    Name { get; }
        /// <summary>
        /// Shared context of AI data
        /// </summary>
        public virtual EnemyContext CTX {
            get => ctx;
            set => ctx = value;
        }
        protected EnemyContext ctx;
        private   Timer        attackCooldownTimer;
        private   RaycastHit2D objectInRange;


        /// <summary>
        /// Launches when the entity call own start method
        /// </summary>
        public virtual void Start()
        {
            attackCooldownTimer = new Timer(ctx.weaponData.attackFrequency,HandleAttack);
            if (ctx.target == null) ctx.reachedEndOfPath = true;
        }

        /// <summary>
        /// Every physics frame update, used for calculate AI movement logic
        /// </summary>
        public virtual void Process() {
            if(ctx.path == null || IsPathFinished()) return;

            Vector2 force = CalculateDirectionAndForce();

            ctx.rigidbody2D.AddForce(force);
            
            HandleWaypointReach();
            attackCooldownTimer.Update(1f);
        }

        private void HandleAttack()
        {
            if (IsEnemyIsInRange(out objectInRange)) //TODO rzuca null reference
            {
                if (objectInRange.IsUnityNull()) return;
                AttackIfValidTarget(objectInRange);
            }
        }
        /// <summary>
        /// Determines if object is valid target to attack and executes attack if so
        /// </summary>
        /// <param name="objectInRange">target to check and attack</param>
        /// <exception cref="NotATargetException">throws if target is not valid</exception>
        protected virtual void AttackIfValidTarget(RaycastHit2D objectInRange)
        {
            var target = objectInRange.transform.GetComponent<Target>();
            if (target.IsUnityNull())
            {
                ctx.weapon.Attack(target);
            }
            else
            {
                throw new NotATargetException();
            }
        }
        
        /// <summary>
        /// Determines when the path is completed
        /// </summary>
        /// <returns>true if path is completed, false in every other situation</returns>
        private bool IsPathFinished()
        {
            if (ctx.path.vectorPath == null) return true;
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

            if (distance < ctx.aiData.nextWaypointDistance)
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
            return direction * (ctx.aiData.speed * Time.deltaTime);
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
        
        
        /// <summary>
        /// Checks if target is in acceptable range from ctx data
        /// </summary>
        /// <param name="targetInRange">outputs object that is in range</param>
        /// <returns></returns>
        private bool IsEnemyIsInRange(out RaycastHit2D targetInRange)
        {
            targetInRange              = GetClosestTarget();
            var rangedWeaponData = ctx.weaponData as EnemyRangedWeaponData;
            if (targetInRange.transform != null && Vector2.Distance(targetInRange.transform.position, ctx.rigidbody2D.position) < rangedWeaponData.attackRange)
            {
                
                return true;
            }
            return false;
        }

        /// <summary>
        /// Cast a circle for capturing objects
        /// </summary>
        /// <returns>RaycastHit2D that represent closets target with valid tag</returns>
        protected RaycastHit2D GetClosestTarget()
        {
            var hits = Physics2D.CircleCastAll(ctx.rigidbody2D.position, ctx.aiData.searchRange, Vector2.zero, ctx.aiData.playerLayerMask.value);

            return hits.FirstOrDefault(x => x.collider.CompareTag("Player") || x.collider.CompareTag("NPC"));
        }
    }
    
}