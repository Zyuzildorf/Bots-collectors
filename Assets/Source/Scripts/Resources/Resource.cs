using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Resource : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    public void SetKinematicBehavior(bool isKinematic)
    {
        _rigidbody.isKinematic = isKinematic;
        _collider.isTrigger = isKinematic;
    }
}