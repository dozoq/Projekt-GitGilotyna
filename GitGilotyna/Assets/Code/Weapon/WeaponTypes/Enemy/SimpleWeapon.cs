using Code.Player;

namespace Code.Weapon.WeaponTypes.Enemy
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