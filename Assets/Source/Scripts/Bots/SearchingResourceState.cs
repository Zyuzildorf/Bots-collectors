using UnityEngine;

public class SearchingResourceState : MoveState
{
    private Vector3 _targetPosition;
    private bool _isPickedUp;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            PickUpResource(resource);
        }
    }
    
    public override void UpdateState()
    {
        Move(_targetPosition);

        if (_isPickedUp)
        {
            BotCollector.SetState(new SearchingResourceState());
        }
    }
    
    public override void Enter()
    {
        _isPickedUp = false;
        _targetPosition = BotCollector.TargetPosition.position;
    }

    private void PickUpResource(Resource resource)
    {
        bool isKinematic = true;
        
        resource.SetKinematicBehavior(isKinematic);
        
        resource.transform.SetParent(transform);
        resource.transform.localPosition = new Vector3(BotCollector.PickUpXOffset, BotCollector.PickUpYOffset,
            BotCollector.PickUpZOffset);
        
        _isPickedUp = true;
    }
}