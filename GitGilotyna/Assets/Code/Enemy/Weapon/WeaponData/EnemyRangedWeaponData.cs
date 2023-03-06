using UnityEngine;

namespace Code.Enemy
{
    
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Enemy/Weapon/Ranged Weapon Data")]
    public sealed class EnemyRangedWeaponData : EnemyWeaponData
    {
        public GameObject bulletPrefab;
        public float      attackRange;
    }
}