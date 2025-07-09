using System.Collections;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class ResourcesSpawner : Spawner
    {
        [SerializeField] private int _amount;
        [SerializeField] private float _delay;
    
        private void Start()
        {
            StartSpawning();
        }
    
        private void StartSpawning()
        {
            StartCoroutine(SpawnObjectsOverTime());
        }

        private IEnumerator SpawnObjectsOverTime()
        {
            int spawnedObjectsCount = 0;
        
            while (_amount > spawnedObjectsCount)
            {
                SpawnObject();
                spawnedObjectsCount++;
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}