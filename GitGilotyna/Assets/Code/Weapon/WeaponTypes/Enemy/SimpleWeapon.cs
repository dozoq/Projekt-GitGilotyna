﻿using Code.Mobs;

namespace Code.Weapon.WeaponTypes.Enemy
{
    public class SimpleWeapon : EnemyWeapon
    {
        public override void Attack(Target target)
        {
            target.TakeDamage(CTX.weaponData.attackDamage);
        }
    }
}