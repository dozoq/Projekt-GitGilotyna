using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Mobs;
using Code.Utilities;
using Code.Weapon.WeaponData;
using Code.Weapon.WeaponTypes.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour, IDeadable
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private Transform spriteTransform;
        private Vector2 moveVector;
        private DirectionRotor rotor;
        private Rigidbody2D rb;
        private PlayerWeapon weapon;
        public WeaponData weaponData;
        private Timer attackTimer;
        private bool readyForAttack;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rotor = new DirectionRotor();
            weapon = WeaponFactory.Get("SimplePlayerWeapon") as PlayerWeapon;
            if (weapon == null) throw new Exception("Weapon Casting Went Wrong!");
            weapon.player = this;
            attackTimer = new Timer(weaponData.attackFrequency, () => readyForAttack = true);
        }

        // Update is called once per frame
        void Update()
        {
            rotor.SetDirectionByFlippingSprite(spriteTransform, moveVector.x);
            attackTimer.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveVector * (speed * Time.fixedDeltaTime));
            var closestTarget = PhysicsUtils.GetClosestTarget(rb.position, 15, LayerMask.GetMask("Mobs"), new List<string>(){"Enemy"});
            if (closestTarget.collider != null)
            {
                var target = closestTarget.collider.GetComponent<Target>();
                if (target != null && readyForAttack)
                {
                    weapon.Attack(target);
                    readyForAttack = false;
                }
            }
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            moveVector = ctx.ReadValue<Vector2>();
        }

        public void MakeDead()
        {
            Debug.Log("Dead");
        }
    }
}
