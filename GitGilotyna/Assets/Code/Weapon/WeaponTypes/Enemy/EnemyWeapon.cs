using Code.Enemy;

namespace Code.Weapon.WeaponTypes.Enemy
{
    public abstract class EnemyWeapon : WeaponType
    {
        public virtual EnemyContext CTX {
            get => _ctx;
            set => _ctx = value;
        }

        public override string Name => this.GetType().Name;
        protected       EnemyContext _ctx;
    }
}