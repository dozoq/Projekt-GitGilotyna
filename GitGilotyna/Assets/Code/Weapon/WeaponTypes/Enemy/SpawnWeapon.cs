using Code.Mobs;
using Code.Weapon.WeaponData;
using UnityEngine;

namespace Code.Weapon.WeaponTypes.Enemy
{
    public class SpawnWeapon : EnemyWeapon
    {
        public override void Attack(Target target)
        {
            var rangedWeapon = _ctx.weaponData as RangedWeaponData;
            var gameObject =
                GameObject.Instantiate(rangedWeapon.bulletPrefab, _ctx.rigidbody2D.position, Quaternion.identity);
        }
    }
}