using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeDino
{
    public class Spawner : MonoBehaviour
    {
        [System.Serializable]
        public struct SpawnableObject
        {
            public GameObject obstaclePrefab;
            [Range(0f, 1f)] 
            public float spawnChance;
        }

        [SerializeField] private SpawnableObject[] _spawnableObjects;
        [SerializeField] private float _minSpawnRate = 1f;
        [SerializeField] private float _maxSpawnRate = 2f;

        private void OnEnable()
        {
            Invoke(nameof(Spawn), Random.Range(_minSpawnRate, _maxSpawnRate));  
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Spawn()
        {
            float spawnChance = Random.value;

            foreach(var obj in _spawnableObjects)
            {
                if(spawnChance < obj.spawnChance)
                {
                    GameObject obstacle = Instantiate(obj.obstaclePrefab);
                    obstacle.transform.position += transform.position;
                    break;
                }

                spawnChance -=obj.spawnChance;
            }

            Invoke(nameof(Spawn), Random.Range(_minSpawnRate,_maxSpawnRate));
        }
    }
}