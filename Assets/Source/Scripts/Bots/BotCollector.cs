using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(CollisionHandler), typeof(StateMachine))]
    public class BotCollector : MonoBehaviour
    {
        private CollisionHandler _collisionHandler;
        private StateMachine _stateMachine;
        
        public bool IsBotFree { get; private set; }

        private void Awake()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            _stateMachine = GetComponent<StateMachine>();

            CompleteTask();
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += _stateMachine.ProcessCollision;
            _stateMachine.TaskCompleted += CompleteTask;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= _stateMachine.ProcessCollision;
            _stateMachine.TaskCompleted -= CompleteTask;
        }

        public void GetTask(Vector3 target)
        {
            IsBotFree = false;
            _stateMachine.GetTask(target);
        }

        private void CompleteTask()
        {
            IsBotFree = true;
        }
    }
}