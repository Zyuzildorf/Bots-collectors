using UnityEngine;

public class SearchingResourceState : MoveState
{
    [SerializeField]  private DeliveringResourceState _deliveringResourceState;
    [SerializeField] private float _closeDistance = 0.1f;
    
    private Vector3 _targetPosition;
    private bool _isTakenResource;

    private void OnTriggerEnter(Collider other)
    {
        if (BotCollector._currentState.Equals(this) == false || other.TryGetComponent(out BotCollector bot))
            return;

        if (other.TryGetComponent(out Resource resource) && resource.IsPickedUp == false
                                                         && resource.IsPreferToDeliver
                                                         && IsTargetResource(resource)) 
        {
            Debug.Log(resource + "picked up");
            PickUpResource(resource);
        }
    }
    
    public override void UpdateState()
    {
        Move(_targetPosition);

        if (_isTakenResource)
        {
            BotCollector.SetState(_deliveringResourceState);
        }
    }
    
    public override void Enter()
    {
        _isTakenResource = false;
        _targetPosition = BotCollector.TargetPosition;
    }

    private void PickUpResource(Resource resource)
    {
        bool isKinematic = true;
        
        resource.GetPickedUp();
        
        resource.SetKinematicBehavior(isKinematic);
        
        resource.transform.SetParent(transform);
        resource.transform.localPosition = new Vector3(BotCollector.PickUpXOffset, BotCollector.PickUpYOffset,
            BotCollector.PickUpZOffset);
        
        _isTakenResource = true;
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