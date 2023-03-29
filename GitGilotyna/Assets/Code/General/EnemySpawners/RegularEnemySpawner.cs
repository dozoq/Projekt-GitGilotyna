using System.Collections;
using Code.Utilities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using MathUtils = Code.Utilities.MathUtils;

namespace Code.General.EnemySpawner
{
    public class RegularEnemySpawner: AbstractSpawner
    {
        [SerializeField] private GameObject spawnPrefab = null;

        protected sealed override void SpawnEnemies()
        {
            for (int i = 0; i < spawnPerTick; i++)
            {

                Instantiate(spawnPrefab,
                    GetNearbyPosition(transform.position,spawnPositionOffset),
                    quaternion.identity);
            }
        }
        
        

    }
}