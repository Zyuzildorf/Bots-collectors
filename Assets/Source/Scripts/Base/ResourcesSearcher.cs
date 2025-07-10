using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesSearcher : MonoBehaviour
    {
        [SerializeField] private float _searchRadius;
        [SerializeField] private int _maxColliders = 10;

        private List<Resource> _freeResources = new List<Resource>();
        private List<Resource> _busyResources = new List<Resource>();
        private Collider[] _hitColliders;

        public int FreeResourcesCount => _freeResources.Count;
        
        public bool TryGetFreeResource(out Resource freeResource)
        {
            if (_freeResources.Count > 0)
            {
                freeResource = GetLastFreeResource();
                return true;
            }

            freeResource = null;
            return false;
        }

        public bool TryFindResources()
        {
            bool isFoundAny = false;

            _hitColliders = new Collider[_maxColliders];

            int collidersAmount = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _hitColliders);

            for (int i = 0; i < collidersAmount; i++)
            {
                if (_hitColliders[i].TryGetComponent(out Resource resource))
                {
                    if (_busyResources.Contains(resource) == false && _freeResources.Contains(resource) == false)
                    {
                        _freeResources.Add(resource);
                        isFoundAny = true;
                    }
                }
            }

            return isFoundAny;
        }
        private Resource GetLastFreeResource()
        {
            Resource resource = _freeResources.Last();
            _freeResources.Remove(resource);
            _busyResources.Add(resource);
            return resource;
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}