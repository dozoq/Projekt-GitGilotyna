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
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour, IDeadable
    {
        public Rigidbody2D GetRigidbody => rb;
        [SerializeField] private UnityEvent OnAttack;
        [SerializeField] private UnityEvent OnMove;

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

        private void GetWeaponDataCopyFromPlayerPrefs()
        {
           var path = PlayerPrefs.GetString(ApplyWeaponForPlayer.WEAPON_KEY);
           var weaponData = Instantiate(Resources.Load<WeaponData>(path));
           if (weaponData != null) firstWeaponData = weaponData;

        }

        private void CreateWeapon()
        {
            firstWeaponData = Instantiate(firstWeaponData);
            var weapon = WeaponFactory.Get("SimplePlayerWeapon") as PlayerWeapon;
            if (weapon == null) throw new Exception("Weapon Casting Went Wrong!");
            weapon.player = this;
            GetWeaponDataCopyFromPlayerPrefs();
            weapon.data = firstWeaponData;
            _firstWeapon = weapon;
            
            
        }


        public void LevelUpAttachment(WeaponDecoratorType type)
        {
            var deco =WeaponDecorator.GetAttachmentOfType<PlayerWeapon>(_firstWeapon, type);
            if (deco != null) deco.Level++;
        }
        
        
        public void AddAttachment(WeaponDecoratorType type)
        {
            switch (type)
            {
                case WeaponDecoratorType.Choke:
                    _firstWeapon = new ChokeDecorator(_firstWeapon);
                    break;
                case WeaponDecoratorType.Scope:
                    _firstWeapon = new ScopeDecorator(_firstWeapon);
                    break;
                case WeaponDecoratorType.Barrel:
                    _firstWeapon = new BarrelDecorator(_firstWeapon);
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
            var closestTarget = PhysicsUtils.GetClosestTarget(rb.position, firstWeaponData.attackRange, LayerMask.GetMask("Mobs"), new List<string>(){"Enemy"});
            if (closestTarget.collider != null)
            {
                var target = closestTarget.collider.GetComponent<Target>();
                if (target != null && _readyForAttack)
                {
                    if(_moveVector.magnitude > 0) _stateContext.Transition(_attackAndRunState);
                    else _stateContext.Transition(_attackState);
                    _firstWeapon.Attack(target);
                    _readyForAttack = false;
                    OnAttack.Invoke();
                } else if(_moveVector.magnitude > 0) _stateContext.Transition(_runState); 
            }
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            OnMove.Invoke();
            float modifier = 1.0f;
            if (PlayerPrefs.HasKey(SkillType.SPEED.ToString()))
                modifier += 100.0f / PlayerPrefs.GetInt(SkillType.SPEED.ToString());
            _moveVector = ctx.ReadValue<Vector2>();
            _moveVector.x *= modifier;
        }

        public void MakeDead()
        {
            Debug.Log("Dead");
            _stateContext.Transition(_deadState);
        }
    }
}
