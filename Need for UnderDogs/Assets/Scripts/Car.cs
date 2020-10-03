using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    [Header("Car properties")]
    //state variables
    [SerializeField] protected float brakeFactor = 15f;
    [SerializeField] protected float acceleration = 1f;
    [SerializeField] protected float maxSpeed = 10f;

    // member variables
    protected Rigidbody _rb;
    protected float _currentSpeed = 0f;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        FreezeConstraints();
    }

    private void FreezeConstraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY
                        | RigidbodyConstraints.FreezeRotationX
                        | RigidbodyConstraints.FreezeRotationZ;
    }

    protected abstract void ProcessInput();
}
