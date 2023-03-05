using System.Collections;
using System.Collections.Generic;
using Code.Utilities;
using Pathfinding;
using UnityEngine;

namespace Code.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform      spriteTransform;
        [SerializeField] private AIContext      aiCtx;
        private const            string         AITypeString = "StandardSeeker";
        private                  AIPath         path;
        private                  DirectionRotor rotor;
        private                  AIType         ai;
        private                  Seeker         seeker;
        


        // Start is called before the first frame update
        void Start()
        {
            path   = GetComponent<AIPath>();
            rotor  = new DirectionRotor();
            ai     = EnemyAIFactory.GetAI(AITypeString);
            ai.CTX = aiCtx;
            ai.Start();
            InvokeRepeating("AIRepeating", 0f, 0.5f);
            
        }

        void AIRepeating()
        {
            ai.Reapeat();
        }

        

        

        // Update is called once per frame
        void Update()
        {
            rotor.SetDirectionByFlippingSprite(spriteTransform, path.desiredVelocity.x);
            ai.Process();
        }
    }
}
