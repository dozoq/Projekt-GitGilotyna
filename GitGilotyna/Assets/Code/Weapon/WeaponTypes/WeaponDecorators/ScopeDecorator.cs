using Code.Mobs;
using UnityEngine;

namespace Code.Enemy.WeaponTypes.WeaponDecorators
{
    public class ScopeDecorator: WeaponDecorator
    {
        public ScopeDecorator(IWeapon weapon) : base(weapon)
        {
            type = WeaponDecoratorType.Choke;
            weapon.data.attackRange += 1;
        }

        public override void Attack(Target target)
        {
            weapon.Attack(target);
        }

        public override void OnLevelUp()
        {
            weapon.data.attackRange += 5;
        }
    }
}