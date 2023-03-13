using Code.Player;
using UnityEngine;

namespace Code.Enemy.WeaponTypes
{
    public class SpawnWeapon : EnemyWeapon
    {
        public override string Name => nameof(SpawnWeapon);
        public override void Attack(Target target)
        {
            var rangedWeapon = ctx.weaponData as EnemyRangedWeaponData;
            var gameObject =
                GameObject.Instantiate(rangedWeapon.bulletPrefab, ctx.rigidbody2D.position, Quaternion.identity);
        }
    }
}