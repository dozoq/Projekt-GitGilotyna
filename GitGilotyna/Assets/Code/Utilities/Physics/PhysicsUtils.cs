using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using UnityEngine;

namespace Code.Utilities
{
    public static class PhysicsUtils
    {
        /// <summary>
        /// Cast a circle for capturing objects
        /// </summary>
        /// <returns>RaycastHit2D that represent closets target with valid tag</returns>
        public static RaycastHit2D GetClosestTarget(Vector2 position, float range, LayerMask layermask, List<string> tags)
        {
            var hits = Physics2D.CircleCastAll(position, range, Vector2.zero,0, layermask.value);
            
            return hits.FirstOrDefault(hit => tags.Any(tag => hit.collider.CompareTag(tag)));
        }

        public static int GetTargetCount(Vector2 position, float range, LayerMask layermask, List<string> tags)
        {
            var hits = Physics2D.CircleCastAll(position, range, Vector2.zero,0, layermask.value);
            
            return hits.Count(hit => tags.Any(tag => hit.collider.CompareTag(tag)));
        }
        
        
        /// <summary>
        /// Cast a circle for capturing objects
        /// </summary>
        /// <returns>RaycastHit2D that represent closets target with valid tag</returns>
        public static RaycastHit2D GetClosestTarget(EnemyContext ctx)
        {
            return GetClosestTarget(ctx.rigidbody2D.position, ctx.aiData.searchRange, ctx.aiData.playerLayerMask,
                ctx.aiData.tags);
        }
        
        
        /// <summary>
        /// Calculate random position near entity position
        /// </summary>
        /// <returns>Vector3 that represents new position</returns>
        public static Vector2 DesignateNearVector2AtRandom(Vector2 position, float offset)
        {
            var newPosition = position;
            newPosition.x = MathUtils.GetRandomFloatWithOffset(newPosition.x, offset);
            newPosition.y = MathUtils.GetRandomFloatWithOffset(newPosition.y, offset);
            return newPosition;
        }
    }
}