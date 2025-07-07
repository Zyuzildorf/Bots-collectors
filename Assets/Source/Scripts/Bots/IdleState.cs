public class IdleState : CollectorState
{
    public override void UpdateState()
    {
        if (BotCollector.IsTaskRecieved)
        {
            BotCollector.SetState(new SearchingResourceState());
        }
    }
}