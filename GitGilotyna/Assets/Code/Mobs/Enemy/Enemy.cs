using System;
using Code.Enemy.AITypes;
using Code.General;
using Code.Mobs;
using Code.Utilities;
using Code.Weapon.WeaponData;
using Code.Weapon.WeaponTypes.Enemy;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Enemy
{
    [Serializable]
    public struct EnemyContext
    {
        [SerializeField, ComponentReferenceInspector] internal Rigidbody2D     rigidbody2D;
        [SerializeField, ComponentReferenceInspector] internal AIData          aiData;
        [SerializeField, ComponentReferenceInspector] internal WeaponData weaponData;
        [SerializeField, ComponentReferenceInspector] internal Transform       target;
        [SerializeField, ComponentReferenceInspector] internal Seeker          seeker;
        internal                  Path            path;
        internal                  bool            reachedEndOfPath;        
        internal                  int             currentWaypoint;
        internal                  EnemyWeapon     weapon;
    }
    public class Enemy : MonoBehaviour, IDeadable
    {
        [SerializeField, ComponentReferenceInspector] private Transform      spriteTransform;
        [SerializeField] private EnemyContext   enemyCtx;
        [SerializeField] private GameObject lootBag;
        private Animator _animator;
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
            GameManager.instance.remainingCount++;
            _animator = GetComponentInChildren<Animator>();
        }

        private void SetupSharedData()
        {
            ai                  = EnemyAIFactory.Get(enemyCtx.aiData.aiType);
            enemyCtx.weapon     = WeaponFactory.Get(enemyCtx.weaponData.weaponType) as EnemyWeapon;
            if (enemyCtx.weapon == null) throw new Exception("Weapon Casting Went Wrong!");
            enemyCtx.weapon.CTX = enemyCtx;
            ai.CTX              = enemyCtx;
        }

        void AIRepeating()
        {
            ai.Repeat();
        }

        

        

        // Update is called once per frame
        void FixedUpdate()
        {
            rotor.SetDirectionByFlippingSprite(spriteTransform, ai.velocity.x);
            ai.Process();
        }

        private void Update()
        {
            SendDataToAnimator();
        }

        private void SendDataToAnimator()
        {
            _animator.SetFloat("StrifeSpeed",Mathf.Abs(ai.velocity.x));
            _animator.SetFloat("ForwardSpeed",Mathf.Abs(ai.velocity.y));
            _animator.SetBool("PointsDown",ai.velocity.x>0);
        }

        public void MakeDead()
        {
            if (lootBag != null) Instantiate(lootBag, transform.position, quaternion.identity);
            GameManager.instance.remainingCount--;
            GameManager.instance.killCount++;
            Destroy(gameObject);
        }
    }
}
