using UnityEngine;

namespace Code.Weapon.WeaponData
{
    
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Enemy/Weapon/Ranged Weapon Data")]
    public sealed class RangedWeaponData : WeaponData
    {
        public GameObject bulletPrefab;
        public float speed = 10f;
    }
}