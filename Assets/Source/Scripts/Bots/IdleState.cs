using UnityEngine;

public class IdleState : CollectorState
{
    [SerializeField] private SearchingResourceState _searchingResourceState;

    public override void UpdateState()
    {
        if (BotCollector.IsTaskRecieved)
        {
            BotCollector.SetState(_searchingResourceState);
        }
    }
}