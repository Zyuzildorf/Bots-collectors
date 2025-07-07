using System;
using UnityEngine;

public class ResourcesSearcher : MonoBehaviour
{
    [SerializeField] private float _searchRadius;

    public Transform SearchResources()
    {
        Transform targetPosition = null;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource))
            {
                targetPosition = collider.transform;
                return targetPosition;
            }
        }
        
        return targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}