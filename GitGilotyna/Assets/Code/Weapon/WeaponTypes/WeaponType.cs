using Code.Enemy;
using Code.Mobs;
using Code.Utilities;
using Code.Weapon.WeaponData;

public abstract class WeaponType : IFactoryData, IWeapon
{
    public abstract string Name { get; }
    public WeaponData data { get; set; }
    public abstract void Attack(Target target);

}
