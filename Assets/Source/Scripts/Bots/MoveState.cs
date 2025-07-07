using UnityEngine;

public class MoveState : CollectorState
{
    protected virtual void Move(Vector3 target)
    {
        transform.Translate(target * (BotCollector.Speed * Time.deltaTime));
        transform.LookAt(target);
    }
}