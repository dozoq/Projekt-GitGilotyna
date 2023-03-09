using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy.AITypes;
using Code.Enemy.WeaponTypes;
using Code.Utilities;
using Pathfinding;
using UnityEngine;

namespace Code.Enemy
{
    [Serializable]
    public struct EnemyContext
    {
        [SerializeField] internal Rigidbody2D     rigidbody2D;
        [SerializeField] internal AIData          aiData;
        [SerializeField] internal EnemyWeaponData weaponData;
        [SerializeField] internal Transform       target;
        [SerializeField] internal Seeker          seeker;
        internal                  Path            path;
        internal                  bool            reachedEndOfPath;        
        internal                  int             currentWaypoint;
        internal                  EnemyWeapon     weapon;
    }
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform      spriteTransform;
        [SerializeField] private EnemyContext   enemyCtx;
        private                  AIPath         path;
        private                  DirectionRotor rotor;
        private                  AIType         ai;
        private const            float          AIInvokeRepeatTime = 0.5f;
        


        // Start is called before the first frame update
        void Start()
        {
            path                = GetComponent<AIPath>();
            rotor               = new DirectionRotor();
            SetupSharedData();
            ai.Start();
            InvokeRepeating(nameof(AIRepeating), 0f, AIInvokeRepeatTime);
            
        }

        private void SetupSharedData()
        {
            ai                  = EnemyAIFactory.Get(enemyCtx.aiData.aiType);
            enemyCtx.weapon     = EnemyWeaponFactory.Get(enemyCtx.weaponData.weaponType);
            enemyCtx.weapon.CTX = enemyCtx;
            ai.CTX              = enemyCtx;
        }

        void AIRepeating()
        {
            ai.Reapeat();
        }

        

        

        // Update is called once per frame
        void FixedUpdate()
        {
            rotor.SetDirectionByFlippingSprite(spriteTransform, path.desiredVelocity.x);
            ai.Process();
        }
    }
}
