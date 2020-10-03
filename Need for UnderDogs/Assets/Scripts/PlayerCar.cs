using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCar : Car
{
    [SerializeField] Transform endPos;
    NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInput();
        agent.autoTraverseOffMeshLink = false;
        transform.LookAt(endPos);
        agent.SetDestination(endPos.position);
    }

    protected override void ProcessInput()
    {
        if (Input.GetMouseButton(0))
        {
            _currentSpeed += acceleration * Time.deltaTime;
            _rb.velocity += Vector3.forward * _currentSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _rb.AddForce(-brakeFactor * _rb.velocity);
        }

        if (_currentSpeed > maxSpeed)
        {
            _currentSpeed = maxSpeed;
        }
    }
}
