using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AICar : Car
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform finishPoint;

    NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        SetNavMeshProperties();
    }

    // Set the navmesh agent properties
    private void SetNavMeshProperties()
    {
        agent.autoTraverseOffMeshLink = false;
        agent.speed = 0f;
        agent.acceleration = 0f;
        agent.angularSpeed = 200f;
        agent.SetDestination(finishPoint.position);
    }

    private void Update()
    {
        ProcessInput();
        print(name + " : " + GetPathRemainingDistance(agent));
    }

    protected override void ProcessInput()
    {
        // Check raycast information
        RaycastHit info;

        if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), transform.localScale * 2f, transform.forward, out info, Quaternion.identity, 13f))
        {
            if (info.transform.CompareTag("Wall"))
            {
                print("Stopping");
                Stop();
            }
        }
        else
        {
            Move();
        }

        if (_currentSpeed > maxSpeed)
        {
            _currentSpeed = maxSpeed;
        }
    }

    // move car on input
    private void Move()
    {
        _currentSpeed += speed * Time.deltaTime;
        agent.speed = _currentSpeed;
        agent.acceleration = _currentSpeed * 2f;
        agent.autoBraking = false;
        agent.isStopped = false;
        agent.updatePosition = true;
        agent.updateRotation = true;
    }

    // stop the car
    private void Stop()
    {
        _currentSpeed = 0f;
        agent.autoBraking = true;
        agent.isStopped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            CollidedWithWall();
        }
    }

    // when car collides with wall
    private void CollidedWithWall()
    {
        agent.Warp(startPoint.position);
        _currentSpeed = 0f;
        agent.autoBraking = true;
        agent.isStopped = true;
        agent.updatePosition = false;
        agent.updateRotation = false;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        agent.SetDestination(finishPoint.position);
    }

    public float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}
