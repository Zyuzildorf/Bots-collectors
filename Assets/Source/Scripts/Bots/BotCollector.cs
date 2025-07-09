using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class BotCollector : MonoBehaviour
    {
        [SerializeField] private IdleState _idleState;

        public Vector3 BasePosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public CollectorState CurrentState { get; private set; }
        public bool IsTaskRecieved { get; private set; }

        private void Awake()
        {
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
            IsTaskRecieved = false;
            TargetPosition = transform.position;
        }

        public void GetTask(Vector3 target)
        {
            IsTaskRecieved = true;
            TargetPosition = target;
        }
    }
}