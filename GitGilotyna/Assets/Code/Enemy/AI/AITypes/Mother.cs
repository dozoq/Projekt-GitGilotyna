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
            Debug.Log(GetChildrenCount());
            if(GetChildrenCount() >= ctx.aiData.maxChildrenCount) return;
            ctx.weapon.Attack(null);
        }

        private int GetChildrenCount() => PhysicsUtils.GetTargetCount(ctx.rigidbody2D.position, ctx.aiData.searchRange,
            ctx.aiData.playerLayerMask, new List<string>() { "Enemy" });
        
        public sealed override void Start()
        {
            base.Start();
            waitForNewPathTimer = new Timer(ctx.aiData.timeToNextSearchPath, StartNewRandomPath);
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
            if(!ctx.seeker.IsDone()) return;
            else if(ctx.reachedEndOfPath)
            {
                waitForNewPathTimer.Update(1f);
            }
        }

        private void Flee()
        {
            var fleeDirection = GetFleeDirection();
            ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.rigidbody2D.position + fleeDirection, OnPathCompleted);
            Debug.Log("Fleeing");
        }

        private Vector2 GetFleeDirection()
        {
            Vector2 targetPosition = ctx.target.transform.position;
            Vector2 targetDirection = (ctx.rigidbody2D.position - targetPosition).normalized;
            return targetDirection * ctx.aiData.searchRange;
        }
        
        private void SearchIfTargetIsInVisibleRange()
        {
            seenTarget = false;
            var hit = PhysicsUtils.GetClosestTarget(ctx);
            Target target = null;
            if (hit.transform != null) target = hit.transform.GetComponent<Target>();
            if (target == null) return;
            seenTarget = true;
            ctx.target = target.transform;
        }
    }
}