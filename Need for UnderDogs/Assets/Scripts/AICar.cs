using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : Car
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform finish;

    NavMeshAgent agent;
    int wayPointIndex = 0;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        agent.autoBraking = false;
        //GoToNextWayPoint();
        agent.SetDestination(finish.position);
    }

    private void GoToNextWayPoint()
    {
        // If no waypoints have been set up
        if (wayPoints.Length == 0) { return; }

        // Choose the next point in the array as the destination, else fix to final waypoint
        if (wayPointIndex < wayPoints.Length)
        {
            Vector3 destinationPos = wayPoints[wayPointIndex].position;
            // Set the agent to go to the current selected destination
            transform.LookAt(destinationPos);
            agent.SetDestination(destinationPos);
            wayPointIndex++;
        }
        else
        {
            wayPointIndex = wayPoints.Length - 1;
        }
    }

    private void Update()
    {
        //ProcessInput();
    }

    protected override void ProcessInput()
    {
        _currentSpeed += speed * Time.deltaTime;
        _rb.velocity += transform.forward * _currentSpeed;
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    _rb.AddForce(-brakeFactor * _rb.velocity);
        //}

        if (_currentSpeed > maxSpeed)
        {
            _currentSpeed = maxSpeed;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "WayPoint")
    //    {
    //        GoToNextWayPoint();
    //        return;
    //    }
    //}
}
