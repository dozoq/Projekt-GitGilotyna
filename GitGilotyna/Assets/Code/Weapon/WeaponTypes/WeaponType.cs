using System.Collections;
using System.Collections.Generic;
using Code.Enemy;
using Code.Player;
using Code.Utilities;
using Code.Weapon.WeaponData;
using UnityEngine;

public abstract class WeaponType : IFactoryData, IWeapon
{
    public abstract string Name { get; }
    public WeaponData data { get; set; }
    public abstract void Attack(Target target);

}
