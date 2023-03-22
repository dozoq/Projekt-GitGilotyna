using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Enemy.WeaponTypes.WeaponDecorators;
using Code.Mobs;
using Code.Player.States.StateFactory;
using Code.Utilities;
using Code.Weapon.WeaponData;
using Code.Weapon.WeaponTypes.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour, IDeadable
    {
        public Rigidbody2D GetRigidbody => rb;

        public WeaponData firstWeaponData;
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private Rigidbody2D rb;
        private Vector2 _moveVector;
        private DirectionRotor _rotor;
        private IWeapon _firstWeapon;
        private Timer _attackTimer;
        private bool _readyForAttack;
        private IState<Player> _idleState, _runState, _attackState, _deadState, _attackAndRunState;
        private StateContext<IState<Player>, Player> _stateContext;
        private List<string> attachments = new List<string>();

        private bool isDead => _stateContext.CurrentState == _deadState;

        // Start is called before the first frame update
        void Start()
        {
            InitializeSubSystems();
            CreateWeapon();
            _attackTimer = new Timer(firstWeaponData.attackFrequency, () => _readyForAttack = true);
        }

        private void InitializeSubSystems()
        {
            InitializeStates();
            _rotor = new DirectionRotor();
            AssignStates();
        }

        private void AssignStates()
        {
            _runState = StateFactory.Get("RunState");
            _idleState = StateFactory.Get("IdleState");
            _attackState = StateFactory.Get("AttackState");
            _deadState = StateFactory.Get("DeadState");
            _attackAndRunState = StateFactory.Get("AttackAndRunState");
            _stateContext.CurrentState = _idleState;
        }

        private void InitializeStates()
        {
            _stateContext = new StateContext<IState<Player>, Player>(this);
            
        }

        private void CreateWeapon()
        {
            var weapon = WeaponFactory.Get("SimplePlayerWeapon") as PlayerWeapon;
            if (weapon == null) throw new Exception("Weapon Casting Went Wrong!");
            weapon.player = this;
            weapon.data = firstWeaponData;
            _firstWeapon = weapon;
            // AddAttachment(WeaponDecoratorType.Choke); Działa
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            // LevelUpAttachment(WeaponDecoratorType.Choke);
            
            
        }


        private void LevelUpAttachment(WeaponDecoratorType type)
        {
            WeaponDecorator.GetAttachmentOfType<PlayerWeapon>(_firstWeapon, type).Level++;
        }
        
        
        private void AddAttachment(WeaponDecoratorType type)
        {
            switch (type)
            {
                case WeaponDecoratorType.Choke:
                    _firstWeapon = new ChokeDecorator(_firstWeapon);
                    
                    break;
                default:
                    throw new Exception("Invalid Attachment");
            }
        }

        // Update is called once per frame
        void Update()
        {
            _stateContext.Transition();
            if(isDead) return;
            _rotor.SetDirectionByFlippingSprite(spriteTransform, _moveVector.x);
            _attackTimer.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if(isDead) return;
            rb.MovePosition(rb.position + _moveVector * (speed * Time.fixedDeltaTime));
            HandleAttack();
        }

        private void HandleAttack()
        {
            var closestTarget = PhysicsUtils.GetClosestTarget(rb.position, 15, LayerMask.GetMask("Mobs"), new List<string>(){"Enemy"});
            if (closestTarget.collider != null)
            {
                var target = closestTarget.collider.GetComponent<Target>();
                if (target != null && _readyForAttack)
                {
                    if(_moveVector.magnitude > 0) _stateContext.Transition(_attackAndRunState);
                    else _stateContext.Transition(_attackState);
                    _firstWeapon.Attack(target);
                    _readyForAttack = false;
                    
                } else if(_moveVector.magnitude > 0) _stateContext.Transition(_runState); 
            }
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            _moveVector = ctx.ReadValue<Vector2>();
        }

        public void MakeDead()
        {
            Debug.Log("Dead");
            _stateContext.Transition(_deadState);
        }
    }
}
