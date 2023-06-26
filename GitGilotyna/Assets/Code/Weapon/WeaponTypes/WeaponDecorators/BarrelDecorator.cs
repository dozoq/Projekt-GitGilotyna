using Code.Mobs;
using UnityEngine;

namespace Code.Enemy.WeaponTypes.WeaponDecorators
{
    public class BarrelDecorator: WeaponDecorator
    {
        public BarrelDecorator(IWeapon weapon) : base(weapon)
        {
            
            type = WeaponDecoratorType.Choke;
            weapon.data.attackFrequency += 0.1f;
        }

        public override void Attack(Target target)
        {
            weapon.Attack(target);
        }

        public override void OnLevelUp()
        {
            weapon.data.attackFrequency += 0.1f;
        }
    }
}