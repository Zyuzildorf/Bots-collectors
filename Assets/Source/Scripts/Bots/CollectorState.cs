using UnityEngine;

public abstract class CollectorState : MonoBehaviour
{
    protected BotCollector BotCollector;

    private void Awake()
    {
        BotCollector = GetComponent<BotCollector>();
    }

    public virtual void UpdateState(){}
    
    public virtual void Enter(){}

    public virtual void Exit(){}
    
}