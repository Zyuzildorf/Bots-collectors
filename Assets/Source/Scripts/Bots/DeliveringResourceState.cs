using UnityEngine;

public class DeliveringResourceState : MoveState
{
    private Vector3 _targetPosition;
    [SerializeField] private IdleState _idleState;


    private void OnTriggerEnter(Collider other)
    {
        if (BotCollector._currentState.Equals(this) == false)
            return;

        if (other.TryGetComponent(out Base botBase))
        {
            Debug.Log("Collided base");
            botBase.GetResource(DropResource());
            Debug.Log("Dropped resource");

            BotCollector.CompleteTask();
            BotCollector.SetState(_idleState);
            Debug.Log("Completed task, set idle state");
        }
    }

    public override void UpdateState()
    {
        Move(_targetPosition);
    }

    public override void Enter()
    {
        _targetPosition = BotCollector.BasePosition;
    }

    public Resource DropResource()
    {
        Resource resource = transform.GetComponentInChildren<Resource>();
        resource.SetKinematicBehavior(false);
        resource.transform.SetParent(null);

        Exit();

        return resource;
    }
}