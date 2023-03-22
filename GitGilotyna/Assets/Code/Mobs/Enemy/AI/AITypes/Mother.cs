using System.Collections.Generic;
using Code.Player;
using Code.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Enemy.AITypes
{
    public class Mother : AIType
    {
        public override string Name => nameof(Mother);
        private Timer waitForNewPathTimer;
        private bool seenTarget = false;
        protected override void HandleAttack()
        {
            if(GetChildrenCount() >= _ctx.aiData.maxChildrenCount) return;
            _ctx.weapon.Attack(null);
        }

        private int GetChildrenCount() => PhysicsUtils.GetTargetCount(_ctx.rigidbody2D.position, _ctx.aiData.searchRange,
            _ctx.aiData.playerLayerMask, new List<string>() { "Enemy" });
        
        public sealed override void Start()
        {
            base.Start();
            waitForNewPathTimer = new Timer(_ctx.aiData.timeToNextSearchPath, StartNewRandomPath);
        }

        public sealed override void Repeat()
        {
            UpdatePath();
        }

        private void UpdatePath()
        {
            SearchIfTargetIsInVisibleRange();
            if (seenTarget)
                Flee();
            if(!_ctx.seeker.IsDone()) return;
            else if(_ctx.reachedEndOfPath)
            {
                waitForNewPathTimer.Update(1f);
            }
        }

        private void Flee()
        {
            var fleeDirection = GetFleeDirection();
            _ctx.seeker.StartPath(_ctx.rigidbody2D.position, _ctx.rigidbody2D.position + fleeDirection, OnPathCompleted);
        }

        private Vector2 GetFleeDirection()
        {
            Vector2 targetPosition = _ctx.target.transform.position;
            Vector2 targetDirection = (_ctx.rigidbody2D.position - targetPosition).normalized;
            return targetDirection * _ctx.aiData.searchRange;
        }
        
        private void SearchIfTargetIsInVisibleRange()
        {
            seenTarget = false;
            var hit = PhysicsUtils.GetClosestTarget(_ctx);
            Target target = null;
            if (hit.transform != null) target = hit.transform.GetComponent<Target>();
            if (target == null) return;
            seenTarget = true;
            _ctx.target = target.transform;
        }
    }
}