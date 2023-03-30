using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Code.Enemy.AITypes;
using Code.Utilities;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Timer = Code.Utilities.Timer;

namespace Code.Enemy
{
    /// <summary>
    /// Represents Enemy AI Factory derived from:
    /// <see cref="AbstractFactory{T}"/>
    /// </summary>
    public sealed class EnemyAIFactory : AbstractFactory<AIType>
    {

    }

}
