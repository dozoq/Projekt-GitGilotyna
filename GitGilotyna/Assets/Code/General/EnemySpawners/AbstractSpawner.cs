using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Code.General.EnemySpawner
{
    public abstract class AbstractSpawner: MonoBehaviour
    {
        [SerializeField] protected float spawnPositionOffset = 1f;
        [SerializeField] protected int spawnPerTick = 1;
        [SerializeField] protected int spawnTickNumber = 1;
        [SerializeField] protected bool isInfinite = false;
        [SerializeField] protected bool isTriggerable = false;
        [SerializeField] protected bool canSpawn = false;
        [SerializeField] protected float spawnTickTime = 1f;
        private int _spawnTicksDone = 0;

        private void Start()
        {
            if (!isTriggerable)
            {
                StartCoroutine(CoroutineSpawn());
            }
        }

        private IEnumerator CoroutineSpawn()
        {
            while (true)
            {
                CheckSpawnConditionsAndSpawn();
                yield return new WaitForSeconds(spawnTickTime);
            }
        }

        public void TriggerSpawn()
        {
            if(isTriggerable) CheckSpawnConditionsAndSpawn();
        }

        private void CheckSpawnConditionsAndSpawn()
        {
            if (!canSpawn) return;
            SpawnEnemies();
            if (!isInfinite)
            {
                _spawnTicksDone++;
                if(_spawnTicksDone > spawnTickNumber) Destroy(this);
            }

        }

        protected abstract void SpawnEnemies();

        public void StopSpawning()
        {
            canSpawn = false;
        }

        protected Vector2 GetNearbyPosition(Vector2 position, float offset)
        {
            var newPosition = position;
            newPosition.x = Utilities.MathUtils.GetRandomFloatWithOffset(newPosition.x, offset);
            newPosition.y = Utilities.MathUtils.GetRandomFloatWithOffset(newPosition.y, offset);
            return newPosition;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var pos = transform.position;
            Gizmos.DrawIcon(pos, "enemy.png");
            pos.y += .5f;
            Handles.Label(pos, "Spawner");
        }
#endif
    }
}