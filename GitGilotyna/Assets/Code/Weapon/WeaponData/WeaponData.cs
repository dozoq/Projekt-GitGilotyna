using System.Collections.Generic;
using Code.Enemy.WeaponTypes.WeaponDecorators;
using UnityEngine;

namespace Code.Weapon.WeaponData
{
    public abstract class WeaponData : ScriptableObject
    {
        public string weaponType;
        public int    attackDamage;
        public float  attackFrequency;        
        public float attackRange;
        public List<WeaponDecoratorType> possibleAttachments;
        public string fileName;
    }
}