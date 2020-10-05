using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField] Transform target;
	[SerializeField] float distance = 20.0f;
	[SerializeField] float height = 5.0f;
	[SerializeField] float heightDamping = 2.0f;
	[SerializeField] float lookAtHeight = 0.0f;
	[SerializeField] Rigidbody parentRigidbody;
	[SerializeField] float rotationSnapTime = 0.3F;
	[SerializeField] float distanceSnapTime;
	[SerializeField] float distanceMultiplier;

	private Vector3 lookAtVector;

	private float usedDistance;
	private float yVelocity = 0.0F;
	private float zVelocity = 0.0F;

	void Start()
	{
		lookAtVector = new Vector3(0, lookAtHeight, 0);
	}

	void LateUpdate()
	{

		float wantedHeight = target.position.y + height;
		float currentHeight = transform.position.y;

		float wantedRotationAngle = target.eulerAngles.y;
		float currentRotationAngle = transform.eulerAngles.y;

		currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		Vector3 wantedPosition = target.position;
		wantedPosition.y = currentHeight;

		usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (parentRigidbody.velocity.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime);

		wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

		transform.position = wantedPosition;

		transform.LookAt(target.position + lookAtVector);

	}

}
