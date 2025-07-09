using Source.Scripts.Resources;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesSearcher : MonoBehaviour
    {
        [SerializeField] private float _searchRadius;

        public bool TryGetResource(out Resource freeResource)
        {
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
}