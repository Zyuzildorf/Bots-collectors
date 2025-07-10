using UnityEngine;

namespace Source.Scripts.Other
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Resource : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private BoxCollider _collider;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }

        public void SetKinematicBehavior(bool isKinematic)
        {
            _rigidbody.isKinematic = isKinematic;
            _collider.isTrigger = isKinematic;
        }

        public void OnCollected()
        {
            Destroy(gameObject);
        }
    }
}