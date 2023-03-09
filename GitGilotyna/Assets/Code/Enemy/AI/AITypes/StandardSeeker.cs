using System.Linq;
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

        public sealed override void Reapeat()
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
            var hit = GetClosestTarget();
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
        /// <summary>
        /// Calculate random position near entity position
        /// </summary>
        /// <returns>Vector3 that represents new position</returns>
        private Vector3 DesignateNearTransformAtRandom()
        {
            var newPosition = ctx.rigidbody2D.position;
            newPosition.x = GetRandomFloatWithOffset(newPosition.x, ctx.aiData.maxRandomPathDistance);
            newPosition.y = GetRandomFloatWithOffset(newPosition.y, ctx.aiData.maxRandomPathDistance);
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
}