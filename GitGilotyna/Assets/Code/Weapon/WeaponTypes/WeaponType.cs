using System.Collections;
using System.Collections.Generic;
using Code.Player;
using Code.Utilities;
using UnityEngine;

public abstract class WeaponType : IFactoryData
{
    public abstract string Name { get; }
    public abstract void         Attack(Target target);

}
