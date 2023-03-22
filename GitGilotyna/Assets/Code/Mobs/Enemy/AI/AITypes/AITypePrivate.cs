using Code.Utilities;
using UnityEngine;

namespace Code.Enemy.AITypes
{
    public  abstract partial class AIType : IFactoryData
    {
        private   RaycastHit2D objectInRange;
        private   Vector2      lastPosition;
        
        #region Private Methods
            /// <summary>
            /// Checks if target is in acceptable range from ctx data
            /// </summary>
            /// <returns>outputs object that is in range</returns>
            private Transform TakeEnemyIsInRange()
            {
                var targetInRange    = PhysicsUtils.GetClosestTarget(_ctx);
                if (targetInRange.transform == null) return null;
                var weaponData = _ctx.weaponData;
                if (Vector2.Distance(targetInRange.transform.position, _ctx.rigidbody2D.position) < weaponData.attackRange)
                {
                    return targetInRange.transform;
                }
                return null;
            }
        #endregion

    }
    
}