using Code.Player;
using Code.Utilities;

namespace Code.Enemy.WeaponTypes
{
    public abstract class EnemyWeapon : IFactoryData
    {
        public abstract string Name { get; }
        public virtual EnemyContext CTX {
            get => ctx;
            set => ctx = value;
        }

        protected       EnemyContext ctx;
        public abstract void         Attack(Target target);
    }
}