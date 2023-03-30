using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Code.General.EnemySpawner
{
    public class WaveEnemySpawner :AbstractSpawner
    {
        [SerializeField] private List<Wave> _listOfWaves = new List<Wave>();
        private int _waveNumber = 0;
        protected override void SpawnEnemies()
        {
            if(_waveNumber >= _listOfWaves.Count) return;
            foreach (var spawnObject in _listOfWaves[_waveNumber])
            {
                Instantiate(spawnObject,
                    GetNearbyPosition(transform.position,spawnPositionOffset),
                    quaternion.identity);
            }
            _waveNumber++;
        }
    }

    [Serializable]
    internal class Wave: IEnumerable<GameObject>
    {
        [SerializeField] private List<GameObject> _objects;
        
        public GameObject this[int index]  
        {  
            get { return _objects[index]; }  
            set { _objects.Insert(index, value); }  
        } 
        public IEnumerator<GameObject> GetEnumerator()
        {
            return _objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}