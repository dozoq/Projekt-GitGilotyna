using Code.Mobs;
using UnityEngine;

namespace Code.Enemy.WeaponTypes.WeaponDecorators
{
    public class ChokeDecorator: WeaponDecorator
    {
        public ChokeDecorator(IWeapon weapon) : base(weapon)
        {
            type = WeaponDecoratorType.Choke;
            weapon.data.attackDamage += 50;
        }

        public override void Attack(Target target)
        {
            weapon.Attack(target);
        }

        public override void OnLevelUp()
        {
            weapon.data.attackDamage += 5;
        }
    }
}