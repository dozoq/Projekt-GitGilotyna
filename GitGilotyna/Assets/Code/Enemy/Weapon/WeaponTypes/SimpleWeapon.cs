using Code.Player;

namespace Code.Enemy.WeaponTypes
{
    public class SimpleWeapon : EnemyWeapon
    {
        public override string Name => nameof(SimpleWeapon);
        public override void Attack(Target target)
        {
            target.TakeDamage(CTX.weaponData.attackDamage);
        }
    }
}