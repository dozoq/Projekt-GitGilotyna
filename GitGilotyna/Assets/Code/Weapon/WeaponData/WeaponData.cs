using UnityEngine;

namespace Code.Weapon.WeaponData
{
    public abstract class WeaponData : ScriptableObject
    {
        public string weaponType;
        public int    attackDamage;
        public float  attackFrequency;        
        public float attackRange;

    }
}