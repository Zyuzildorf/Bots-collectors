using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class SearchingResourceState : MovementState, IUpdatable, IEnterable
    {
        [SerializeField]  private DeliveringResourceState _deliveringResourceState;
        [SerializeField] private float _closeDistance = 0.1f;
        [SerializeField] private float _pickUpZOffset;
        [SerializeField] private float _pickUpYOffset;
        [SerializeField] private float _pickUpXOffset;
    
        private Vector3 _targetPosition;
        private bool _isResourceTaken;

        private void OnTriggerEnter(Collider other)
        {
            if (BotCollector.CurrentState.Equals(this) == false || other.TryGetComponent(out BotCollector bot))
                return;

            if (other.TryGetComponent(out Resource resource) && IsTargetResource(resource)) 
            {
                PickUpResource(resource);
            }
        }
    
        public void UpdateState()
        {
            Move(_targetPosition);
            Rotate(_targetPosition);

            if (_isResourceTaken)
            {
                BotCollector.SetState(_deliveringResourceState);
            }
        }
    
        public void Enter()
        {
            _isResourceTaken = false;
            _targetPosition = BotCollector.TargetPosition;
        }

        private void PickUpResource(Resource resource)
        {
            bool isKinematic = true;
        
            resource.SetKinematicBehavior(isKinematic);
        
            resource.transform.SetParent(transform);
            resource.transform.localPosition = new Vector3(_pickUpXOffset, _pickUpYOffset,
                _pickUpZOffset);
        
            _isResourceTaken = true;
        }

        private bool IsTargetResource(Resource resource)
        {
            Vector2 targetPosition = new Vector2(_targetPosition.x, _targetPosition.z);
            Vector2 resourcePosition = new Vector2(resource.transform.position.x, resource.transform.position.z);
        
            if ((targetPosition - resourcePosition).sqrMagnitude < _closeDistance * _closeDistance)
            {
                return true;
            }
            return false;
        }
    }
}