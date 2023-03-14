using Code.Player;
using Code.Utilities;
using UnityEngine;

namespace Code.Enemy.AITypes
{
    public sealed class StandardRangeSeeker : StandardSeeker
    {
        public override string Name => nameof(StandardRangeSeeker);
        private bool isEnemyInRange = false;
        public override void Process()
        {
            HandleEnemyInRangeFlag();
            attackCooldownTimer.Update(1f);
            if(ctx.path == null || IsPathFinished() || isEnemyInRange) return;
            HandleMovement();
            HandleWaypointReach();
        }

        private void HandleEnemyInRangeFlag()
        {
            var hit = PhysicsUtils.GetClosestTarget(ctx);
            if (hit.transform != null && hit.transform.GetComponent<Target>() != null)
            {
                isEnemyInRange = true;
            }
            else
            {
                isEnemyInRange = false;
            }
            
        }

        protected override void AttackIfValidTarget(Transform detectedObject)
        {
            base.AttackIfValidTarget(detectedObject);
        }

        protected override void HandleAttack()
        {
            base.HandleAttack();
        }
    }
}