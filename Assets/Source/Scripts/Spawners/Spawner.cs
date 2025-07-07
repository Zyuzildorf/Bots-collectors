using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _radius;
    
    protected void SpawnObject()
    {
        Instantiate(_prefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    protected void SpawnObject(out GameObject obj)
    {
        obj = Instantiate(_prefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(transform.position.x - _radius, transform.position.x + _radius),transform.position.y,
            Random.Range(transform.position.z - _radius, transform.position.z + _radius));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}