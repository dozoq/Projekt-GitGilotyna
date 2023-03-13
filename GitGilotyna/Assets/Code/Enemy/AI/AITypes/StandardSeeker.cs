using Code.Utilities;
using UnityEngine;

namespace Code.Enemy.AITypes
{
    public class StandardSeeker : AIType
    {
        public override string Name => nameof(StandardSeeker);
        private         Timer  waitForNewPathTimer;
        private         Timer  lastSeenTargetTimer;

        public sealed override void Start()
        {
            base.Start();
            waitForNewPathTimer = new Timer(ctx.aiData.timeToNextSearchPath, StartNewRandomPath);
            lastSeenTargetTimer = new Timer(ctx.aiData.memoryTime,           ResetTarget);
        }

        public sealed override void Repeat()
        {
            UpdatePath();
        }

        private void UpdatePath()
        {
            if(!ctx.seeker.IsDone()) return;
            if (ctx.target != null)
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, OnPathCompleted);
            else if(ctx.reachedEndOfPath)
            {
                waitForNewPathTimer.Update(1f);
            }
            SearchIfTargetIsInVisibleRange();
        }
        /// <summary>
        /// Search area to found if any mob is in visible range and handles the memorization of last target
        /// </summary>
        private void SearchIfTargetIsInVisibleRange()
        {
            var hit = PhysicsUtils.GetClosestTarget(ctx);
            HandleMemorization(hit);
        }

        private void HandleMemorization(RaycastHit2D hit)
        {
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

        private void ResetTarget()
        {
            ctx.target = null;
        }
    }
}