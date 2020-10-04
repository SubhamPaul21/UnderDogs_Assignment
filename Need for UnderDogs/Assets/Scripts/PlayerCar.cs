using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCar : Car
{
    Transform startPoint;
    Transform finishPoint;

    NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        SetNavMeshProperties(); 
        GetStartFinishPoint();
        agent.SetDestination(finishPoint.position);
    }

    // Set the navmesh agent properties
    private void SetNavMeshProperties()
    {
        agent.autoTraverseOffMeshLink = false;
        agent.speed = 0f;
        agent.acceleration = 0f;
        agent.angularSpeed = 200f;
    }

    // Get start and finish point
    private void GetStartFinishPoint()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;
        finishPoint = GameObject.FindGameObjectWithTag("FinishPoint").transform;
    }

    private void Update()
    {
        ProcessInput();
    }

    protected override void ProcessInput()
    {
        if (Input.GetMouseButton(0))
        {
            _currentSpeed += speed * Time.deltaTime;
            agent.speed = _currentSpeed;
            agent.acceleration = _currentSpeed * 2f;
            agent.autoBraking = false;
            agent.isStopped = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _currentSpeed = 0f;
            agent.autoBraking = true;
            agent.isStopped = true;
        }

        if (_currentSpeed > maxSpeed)
        {
            _currentSpeed = maxSpeed;
        }
    }
}
