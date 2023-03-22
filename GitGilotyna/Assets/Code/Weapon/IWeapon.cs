using Code.Player;
using Code.Weapon.WeaponData;

namespace Code.Enemy
{
    public interface IWeapon
    {
        public WeaponData data { get; set; }
        public void Attack(Target target);
    }
}