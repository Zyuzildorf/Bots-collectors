using System.Collections.Generic;
using UnityEngine;

public class ResourcesSearcher : MonoBehaviour
{
    [SerializeField] private float _searchRadius;

    public bool TryGetResource(out Resource freeResource)
    {
       // freeResources = new List<Resource>();
       freeResource = null;
           
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource) && resource.IsPreferToDeliver == false)
            {
                freeResource = resource;
                return true;
            }
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}