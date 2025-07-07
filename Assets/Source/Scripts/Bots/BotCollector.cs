using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _pickUpZOffset;
    [SerializeField] private float _pickUpYOffset;
    [SerializeField] private float _pickUpXOffset;
    
    private CollectorState _currentState;
    
    public Transform BasePosition { get; private set; }
    public Transform TargetPosition { get; private set; }
    public float Speed { get; private set; }
    public float PickUpZOffset { get; private set; }
    public float PickUpYOffset { get; private set; }
    public float PickUpXOffset { get; private set; }
    public bool IsTaskRecieved { get; private set; }

    private void Awake()
    {
        Speed = _speed;
        PickUpZOffset = _pickUpZOffset;
        PickUpYOffset = _pickUpYOffset;
        PickUpXOffset = _pickUpXOffset;
        
        SetState(new IdleState());
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

    public void GetTask(Transform target)
    {
        TargetPosition = target;
        IsTaskRecieved = true;
    }
}