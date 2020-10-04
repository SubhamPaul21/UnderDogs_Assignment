using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCar : Car
{
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    int wayPointIndex = 0;
    Vector3 destinationPos;

    protected override void Start()
    {
        base.Start();
        AddWayPoints();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        GoToNextWayPoint();
    }

    private void AddWayPoints()
    {
        Transform wayPointTransform = GameObject.FindGameObjectWithTag("Track").transform.GetChild(1).transform;
        foreach (Transform wayPoint in wayPointTransform)
        {
            wayPoints.Add(wayPoint);
        }
    }

    private void GoToNextWayPoint()
    {
        // If no waypoints have been set up
        if (wayPoints.Count == 0) { return; }

        // Choose the next point in the array as the destination, else fix to final waypoint
        if (wayPointIndex < wayPoints.Count)
        {
            destinationPos = wayPoints[wayPointIndex].position;
            // Set the agent to go to the current selected destination
            //Vector3.MoveTowards(transform.position, destinationPos, 0f);
            agent.SetDestination(destinationPos);
            transform.LookAt(destinationPos);
            wayPointIndex++;
        }
        else
        {
            wayPointIndex = wayPoints.Count - 1;
        }
    }

    private void Update()
    {
        ProcessInput();
        //transform.LookAt(destinationPos);
    }

    protected override void ProcessInput()
    {
        if (Input.GetMouseButton(0))
        {
            _currentSpeed += acceleration * Time.deltaTime;
            _rb.velocity += transform.forward * _currentSpeed;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            _rb.AddForce(-30f * _rb.velocity);
            GoToNextWayPoint();
            return;
        }
    }
}
