using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Code.General.EnemySpawner
{
    public class EnemySpawner: MonoBehaviour
    {
        private bool _isNight;

        public void NightBecame()
        {
            _isNight = true;
        }

        private void Start()
        {
            StartCoroutine(HandleSpawning());
        }

        private IEnumerator HandleSpawning()
        {
            while (_isNight)
            {
                yield return null;
            }
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