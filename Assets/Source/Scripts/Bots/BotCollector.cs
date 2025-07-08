using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _pickUpZOffset;
    [SerializeField] private float _pickUpYOffset;
    [SerializeField] private float _pickUpXOffset;

    public CollectorState _currentState;
    [SerializeField] private IdleState _idleState;

    public Vector3 BasePosition { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RotateSpeed { get; private set; }
    public float PickUpZOffset { get; private set; }
    public float PickUpYOffset { get; private set; }
    public float PickUpXOffset { get; private set; }
    public bool IsTaskRecieved { get; private set; }

    private void Awake()
    {
        BasePosition = transform.position;
        MoveSpeed = _moveSpeed;
        RotateSpeed = _rotateSpeed;
        PickUpZOffset = _pickUpZOffset;
        PickUpYOffset = _pickUpYOffset;
        PickUpXOffset = _pickUpXOffset;

        CompleteTask();
        SetState(_idleState);
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void SetState(CollectorState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }

    public void CompleteTask()
    {
        IsTaskRecieved = false;
    }

    public void GetTask(Vector3 target)
    {
        IsTaskRecieved = true;
        TargetPosition = target;
    }
}