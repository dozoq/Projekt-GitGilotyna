using System;
using Code.Player;
using Code.Weapon.WeaponData;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Weapon.WeaponTypes.Player
{
    public class SimplePlayerWeapon : PlayerWeapon
    {
        public override string Name => nameof(SimplePlayerWeapon);
        public override void Attack(Target target)
        {
            var weaponData = player.weaponData as RangedWeaponData;
            if (weaponData == null) throw new Exception("Invalid Data Type For Weapon!");
            var gameObject =
                GameObject.Instantiate(weaponData.bulletPrefab, player.transform.position, quaternion.identity);
            var rb = gameObject.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<Bullet>().Initialize("Enemy", weaponData.attackDamage);
            var force = (target.transform.position - player.transform.position).normalized * Time.fixedDeltaTime * weaponData.speed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}