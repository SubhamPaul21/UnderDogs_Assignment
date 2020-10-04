using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCar : Car
{
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    [SerializeField] Transform finish;
    int wayPointIndex = 0;
    Vector3 destinationPos;
    Transform nextWayPoint;
    float timeCount = 0f;

    protected override void Start()
    {
        base.Start();
        AddWayPoints();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        //GoToNextWayPoint();
        agent.SetDestination(finish.position);
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
            //transform.LookAt(destinationPos);
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
        //UpdateRotation();
    }

    private void UpdateRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, nextWayPoint.rotation, Mathf.Clamp(timeCount, 0f,1f));
        timeCount += Time.deltaTime;
    }

    protected override void ProcessInput()
    {
        if (Input.GetMouseButton(0))
        {
            _currentSpeed += speed * Time.deltaTime;
            agent.speed = _currentSpeed;
            agent.acceleration = _currentSpeed * 2f;
            agent.autoBraking = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            agent.speed = 0f;
            agent.acceleration = 0f;
            _rb.velocity = Vector3.zero;
            agent.autoBraking = true;
        }

        if (_currentSpeed > maxSpeed)
        {
            _currentSpeed = maxSpeed;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "WayPoint")
    //    {
    //        nextWayPoint = wayPoints[wayPointIndex + 1];
    //        _rb.AddForce(-30f * _rb.velocity);
    //        timeCount = 0f;
    //        GoToNextWayPoint();
    //        return;
    //    }
    //}
}
