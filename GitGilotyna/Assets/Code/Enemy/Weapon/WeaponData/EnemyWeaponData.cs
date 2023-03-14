using UnityEngine;

namespace Code.Enemy
{
    public abstract class EnemyWeaponData : ScriptableObject
    {
        public string weaponType;
        public int    attackDamage;
        public float  attackFrequency;        
        public float attackRange;

    }
}