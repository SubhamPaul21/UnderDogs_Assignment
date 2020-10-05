using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCar : Car
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
    }

    protected override void ProcessInput()
    {
        
        if (Input.GetMouseButton(0) && GameHandler.Instance.IsGameReady)
        {
            Move();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Stop();
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

    public float GetPathRemainingDistance()
    {
        if (agent.pathPending ||
            agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            agent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < agent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(agent.path.corners[i], agent.path.corners[i + 1]);
        }

        return distance + Random.Range(0.001f, 0.1f);
    }
}
