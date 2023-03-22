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
            waitForNewPathTimer = new Timer(_ctx.aiData.timeToNextSearchPath, StartNewRandomPath);
            lastSeenTargetTimer = new Timer(_ctx.aiData.memoryTime,           ResetTarget);
        }

        public sealed override void Repeat()
        {
            UpdatePath();
        }

        private void UpdatePath()
        {
            if(!_ctx.seeker.IsDone()) return;
            if (_ctx.target != null)
                _ctx.seeker.StartPath(_ctx.rigidbody2D.position, _ctx.target.position, OnPathCompleted);
            else if(_ctx.reachedEndOfPath)
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
            var hit = PhysicsUtils.GetClosestTarget(_ctx);
            HandleMemorization(hit);
        }

        private void HandleMemorization(RaycastHit2D hit)
        {
            if(hit.transform != null)
            {
                _ctx.target = hit.transform;
                lastSeenTargetTimer.Reset();
            }
            else
            {
                lastSeenTargetTimer.Update(1f);
            }
        }

        private void ResetTarget()
        {
            _ctx.target = null;
        }
    }
}