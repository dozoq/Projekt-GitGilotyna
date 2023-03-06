using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Enemy.WeaponTypes;
using Code.Player;
using Code.Utilities;
using UnityEngine;

namespace Code.Enemy
{
    /// <summary>
    /// Represents Enemy Weapon Factory derived from:
    /// <see cref="AbstractFactory{T}"/>
    /// </summary>
    public sealed class EnemyWeaponFactory : AbstractFactory<EnemyWeapon>
    {
    }
}