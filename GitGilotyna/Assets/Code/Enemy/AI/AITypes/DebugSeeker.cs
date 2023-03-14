using UnityEngine;

namespace Code.Enemy.AITypes
{
    public sealed class DebugSeeker : AIType
    {
        public override string Name => nameof(DebugSeeker);
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Process()
        {
            Debug.Log("Debug Enemy AI Processing");
        }

        public override void Start()
        {
            Debug.Log("Debug Enemy AI Start");
        }
    }
}