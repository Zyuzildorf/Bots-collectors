using UnityEngine;

public class MoveState : CollectorState
{
    protected virtual void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, BotCollector.MoveSpeed * Time.deltaTime);
    }
}