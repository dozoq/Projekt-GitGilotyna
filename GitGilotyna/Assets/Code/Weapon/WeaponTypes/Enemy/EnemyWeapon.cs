using Code.Enemy;

namespace Code.Weapon.WeaponTypes.Enemy
{
    public abstract class EnemyWeapon : WeaponType
    {
        public virtual EnemyContext CTX {
            get => ctx;
            set => ctx = value;
        }

        protected       EnemyContext ctx;
    }
}