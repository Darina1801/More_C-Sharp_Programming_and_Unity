using UnityEngine;

public class Ship : MonoBehaviour
{
	Vector3 position;

	Rigidbody2D shipRigidbody;
	private Vector3 thrustDirection = new Vector3(1, 0, 0);
	private const int ThrustForce = 5;

	private CircleCollider2D shipCollider;
	private float shipColliderRadius;

	private const int RotateDegreesPerSecond = 10;

	// Start is called before the first frame update
	void Start()
	{
		shipRigidbody = GetComponent<Rigidbody2D>();
		shipCollider = GetComponent<CircleCollider2D>();
		shipColliderRadius = shipCollider.radius;
	}

	void FixedUpdate()
	{
		position = transform.position;
		if (Input.GetAxis("Thrust") != 0)
		{
			shipRigidbody.AddForce(position + ThrustForce * thrustDirection * Time.deltaTime);
		}
	}


	void OnBecameInvisible()
	{
		if (position.x - shipColliderRadius <= ScreenUtils.ScreenLeft)
		{
			position.x = ScreenUtils.ScreenRight + shipColliderRadius;
		}

		else if (position.x + shipColliderRadius >= ScreenUtils.ScreenRight)
		{
			position.x = ScreenUtils.ScreenLeft - shipColliderRadius;
		}

		else if (position.y + shipColliderRadius >= ScreenUtils.ScreenTop)
		{
			position.y = ScreenUtils.ScreenBottom - shipColliderRadius;
		}

		else if (position.y - shipColliderRadius <= ScreenUtils.ScreenBottom)
		{
			position.y = ScreenUtils.ScreenTop + shipColliderRadius;
		}

		transform.position = position;
	}

	//Update is called once per frame
	void Update()
	{
		float rotationInput = Input.GetAxis("Rotate");
		if (rotationInput != 0)
		{
			float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
			if (rotationInput < 0)
			{
				rotationAmount *= -1;
			}

			transform.Rotate(Vector3.forward, rotationAmount);
		}
		thrustDirection.x = Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180);
		thrustDirection.y = Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180);

		position.x += thrustDirection.x;
		position.y += thrustDirection.y;
	}
}