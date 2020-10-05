using UnityEngine;

public abstract class Car : MonoBehaviour
{
    [Header("Car properties")]
    //state variables
    protected float brakeFactor = 15f;
    protected float speed = 10f;
    protected float maxSpeed = 30f;

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
