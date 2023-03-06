using UnityEngine;

namespace Code.Enemy
{
    /// <summary>
    /// Holder of AI Designer Access Data
    /// </summary>
    [CreateAssetMenu(fileName = "AI_Data", menuName = "Enemy/AI Data")]
    public class AIData : ScriptableObject
    {
        public LayerMask playerLayerMask;
        public int       nextWaypointDistance;
        public float     speed;
        public float     maxRandomPathDistance;
        public float     searchRange;
        public float     timeToNextSearchPath;
        public float     memoryTime;
        public string    AIType;
    }
}