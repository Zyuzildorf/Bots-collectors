using Source.Scripts.Interfaces;
using Source.Scripts.Resources;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class DeliveringResourceState : MovementState, IUpdatable, IEnterable
    {
        [SerializeField] private IdleState _idleState;
    
        private Vector3 _targetPosition;

        private void OnTriggerEnter(Collider other)
        {
            if (BotCollector.CurrentState.Equals(this) == false)
                return;

            if (other.TryGetComponent(out Base.Base botBase))
            {
                botBase.GetResource(DropResource());

                BotCollector.CompleteTask();
                BotCollector.SetState(_idleState);
            }
        }

        public void UpdateState()
        {
            Move(_targetPosition);
            Rotate(_targetPosition);
        }

        public void Enter()
        {
            _targetPosition = BotCollector.BasePosition;
        }

        private Resource DropResource()
        {
            Resource resource = transform.GetComponentInChildren<Resource>();
            resource.SetKinematicBehavior(false);
            resource.transform.SetParent(null);

            return resource;
        }
    }
}