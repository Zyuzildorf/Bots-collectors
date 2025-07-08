using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Resource : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    
    public bool IsPreferToDeliver { get; private set; }
    public bool IsPickedUp { get; private set; }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        IsPreferToDeliver = false;
        IsPickedUp = false;
    }

    public void SetKinematicBehavior(bool isKinematic)
    {
        _rigidbody.isKinematic = isKinematic;
        _collider.isTrigger = isKinematic;
    }

    public void GetPreferToDeliver()
    {
        IsPreferToDeliver = true;
    }

    public void GetPickedUp()
    {
        IsPickedUp = true;
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}