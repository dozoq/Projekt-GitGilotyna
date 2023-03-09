using System.Collections.Generic;
using System.Linq;
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
    }
}