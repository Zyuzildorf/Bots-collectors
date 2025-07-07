using UnityEngine;

public class DeliveringResourceState : MoveState
{
    private Vector3 _targetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Base botBase))
        {
            DropResource();
            BotCollector.CompleteTask();
            BotCollector.SetState(new IdleState());
        }
    }
    
    public override void UpdateState()
    {
       Move(_targetPosition); 
    }

    public override void Enter()
    {
        _targetPosition = BotCollector.BasePosition.position;
    }
    
    private void DropResource()
    {
        transform.GetChild(0).GetComponent<Resource>().SetKinematicBehavior(false);
        transform.DetachChildren();
    }
}