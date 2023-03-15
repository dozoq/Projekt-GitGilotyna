using Code.Player;
using Code.Weapon.WeaponData;
using UnityEngine;

namespace Code.Weapon.WeaponTypes.Enemy
{
    public class SpawnWeapon : EnemyWeapon
    {
        public override string Name => nameof(SpawnWeapon);
        public override void Attack(Target target)
        {
            var rangedWeapon = ctx.weaponData as RangedWeaponData;
            var gameObject =
                GameObject.Instantiate(rangedWeapon.bulletPrefab, ctx.rigidbody2D.position, Quaternion.identity);
        }
    }
}