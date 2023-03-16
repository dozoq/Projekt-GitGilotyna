using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Enemy.AITypes;
using Code.Utilities;
using UnityEngine;

namespace Code.Enemy
{
    /// <summary>
    /// Holder of AI Designer Access Data
    /// </summary>
    [CreateAssetMenu(fileName = "AI_Data", menuName = "Enemy/AI Data")]
    public class AIData : ScriptableObject
    {
        public LayerMask playerLayerMask = 0;
        public int nextWaypointDistance = 1;
        public int maxChildrenCount = 5;
        public float speed = 200;
        public float maxRandomPathDistance = 10;
        public float searchRange = 3;
        public float timeToNextSearchPath = 5;
        public float memoryTime = 5;
        public string aiType = nameof(DebugSeeker);
        public bool useForce = false;
        public List<string> tags = new List<string>() { "Player", "NPC" };
    }
}