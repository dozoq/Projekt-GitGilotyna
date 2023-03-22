using Code.Player;
using Code.Utilities;
using Pathfinding;
using UnityEngine;

namespace Code.Enemy.AITypes
{
    public  abstract partial class AIType : IFactoryData
    {
        
        public abstract string    Name { get; }
        /// <summary>
        /// Shared context of AI data
        /// </summary>
        public virtual EnemyContext CTX {
            get => _ctx;
            set => _ctx = value;
        }
        protected EnemyContext _ctx;
        protected Timer attackCooldownTimer;
        
        #region Virtual Methods
        /// <summary>
        /// Launches when the entity call own start method
        /// </summary>
        public virtual void Start()
        {
            attackCooldownTimer = new Timer(_ctx.weaponData.attackFrequency,HandleAttack);
            if (_ctx.target == null) _ctx.reachedEndOfPath = true;
        }

        /// <summary>
        /// Every physics frame update, used for calculate AI movement logic
        /// </summary>
        public virtual void Process() {
            attackCooldownTimer.Update(1f);
            if(_ctx.path == null || IsPathFinished()) return;

            HandleMovement();
            HandleWaypointReach();
        }

        protected virtual void HandleMovement()
        {
            Vector2 force = CalculateDirectionAndForce();

            if(_ctx.aiData.useForce) _ctx.rigidbody2D.AddForce(force);
            else _ctx.rigidbody2D.MovePosition(_ctx.rigidbody2D.position + force);
        }

        /// <summary>
        /// Determines if object is valid target to attack and executes attack if so
        /// </summary>
        /// <param name="detectedObject">target to check and attack</param>
        /// <exception cref="NotATargetException">throws if target is not valid</exception>
        protected virtual void AttackIfValidTarget(Transform detectedObject)
        {
            var target = detectedObject.GetComponent<Target>();
            if (target == null) return;
            _ctx.weapon.Attack(target);
        }

        /// <summary>
        /// Callback function that handles path changing
        /// </summary>
        /// <param name="newPath"></param>
        protected virtual void OnPathCompleted(Path newPath)
        {
            if (!newPath.error)
            {
                _ctx.rigidbody2D.velocity = Vector2.zero;
                _ctx.path            = newPath;
                _ctx.currentWaypoint = 0;
            }
        }

        /// <summary>
        /// exposes access to InvokeRepeating in base entity
        /// </summary>
        public virtual void Repeat()
        {
        }
        
        protected virtual void HandleAttack()
        {
            var target = TakeEnemyIsInRange();
            if (target != null)
            {
                AttackIfValidTarget(target);
            }
        }
        #endregion

        #region Use Only

        /// <summary>
        /// Determines when the path is completed
        /// </summary>
        /// <returns>true if path is completed, false in every other situation</returns>
        protected bool IsPathFinished()
        {
            if (_ctx.path.vectorPath == null) return true;
            if (_ctx.currentWaypoint >= _ctx.path.vectorPath.Count)
            {
                return _ctx.reachedEndOfPath = true;
            }
            return _ctx.reachedEndOfPath = false;
        }
        /// <summary>
        /// Handles changing waypoints to new when current is reached
        /// </summary>
        protected void HandleWaypointReach()
        {
            float distance = Vector2.Distance(_ctx.rigidbody2D.position, _ctx.path.vectorPath[_ctx.currentWaypoint]);

            if (distance < _ctx.aiData.nextWaypointDistance)
            {
                _ctx.currentWaypoint++;
            }
        }
        
        /// <summary>
        /// Calculates Direction by subtracting current position from waypoint position and normalizing, then uses it to calculate force vector
        /// </summary>
        /// <returns>Vector2 representing force in direction of waypoint</returns>
        protected Vector2 CalculateDirectionAndForce()
        {
            Vector2 direction = ((Vector2)_ctx.path.vectorPath[_ctx.currentWaypoint] - _ctx.rigidbody2D.position)
                .normalized;
            return direction * (_ctx.aiData.speed * Time.fixedDeltaTime);
        }
        
        protected void StartNewRandomPath()
        {
            var position = PhysicsUtils.DesignateNearVector2AtRandom(_ctx.rigidbody2D.position, _ctx.aiData.maxRandomPathDistance);
            _ctx.seeker.StartPath(_ctx.rigidbody2D.position, position, OnPathCompleted); 
        }

        #endregion
        
    }
}