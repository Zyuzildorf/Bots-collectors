using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(CollisionHandler))]
    public class BotCollector : MonoBehaviour
    {
        [SerializeField] private IdleState _idleState;

        private CollisionHandler _collisionHandler;

        public Vector3 BasePosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public CollectorState CurrentState { get; private set; }
        public bool IsTaskReceived { get; private set; }

        private void Awake()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            BasePosition = transform.position;

            CompleteTask();
            SetState(_idleState);
        }

        private void Update()
        {
            if (CurrentState is IUpdatable updatable)
            {
                updatable.UpdateState();
            }
        }

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += ProccessCollision;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= ProccessCollision;
        }

        public void SetState(CollectorState state)
        {
            if (CurrentState == state)
            {
                return;
            }

            CurrentState = state;

            if (CurrentState is IEnterable enterable)
            {
                enterable.Enter();
            }
        }

        public void CompleteTask()
        {
            IsTaskReceived = false;
            TargetPosition = transform.position;
        }

        public void GetTask(Vector3 target)
        {
            IsTaskReceived = true;
            TargetPosition = target;
        }

        private void ProccessCollision(Collider other)
        {
            if (CurrentState is ITriggerable triggerable)
            {
                triggerable.ProcessTriggerCollider(other);
            }
        }
    }
}