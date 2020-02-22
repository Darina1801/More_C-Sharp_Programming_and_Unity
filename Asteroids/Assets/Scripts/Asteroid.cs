using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	Sprite asteroid_red;
	[SerializeField]
	Sprite asteroid_blue;
	[SerializeField]
	Sprite asteroid_grey;

	//Initialise method support
	Vector2 moveDirection;
	Direction direction;

	// Start is called before the first frame update
	void Start()
	{
		//randomly pick one of the three asteroid sprites
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		int spriteNumber = Random.Range(0, 3);
		if (spriteNumber < 1)
		{
			spriteRenderer.sprite = asteroid_red;
		}
		else if (spriteNumber < 2)
		{
			spriteRenderer.sprite = asteroid_blue;
		}
		else
		{
			spriteRenderer.sprite = asteroid_grey;
		}
	}

	public void Initialize(Direction direction, Vector3 asteroidPosition)
	{
		this.direction = direction;
		float angle = Random.Range(0, Mathf.PI / 6);
		transform.position = asteroidPosition;
		StartMoving(angle);
	}

	public void StartMoving(float asteroidAngle)
	{
		// apply impulse force to get game object moving
		const float MinImpulseForce = 2f;
		const float MaxImpulseForce = 7f;
		const float baseAngleUp = 75 * Mathf.PI / 180;
		const float baseAngleLeft = 165 * Mathf.PI / 180;
		const float baseAngleDown = 255 * Mathf.PI / 180;
		const float baseAngleRight = 345 * Mathf.PI / 180;
		switch (direction)
		{
			case Direction.Up:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleUp + asteroidAngle), Mathf.Sin(baseAngleUp + asteroidAngle));
				break;
			case Direction.Left:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleLeft + asteroidAngle), Mathf.Sin(baseAngleLeft + asteroidAngle));
				break;
			case Direction.Down:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleDown + asteroidAngle), Mathf.Sin(baseAngleDown + asteroidAngle));
				break;
			case Direction.Right:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleRight + asteroidAngle), Mathf.Sin(baseAngleRight + asteroidAngle));
				break;
		}

		float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
		GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject bullet = GameObject.FindWithTag("Bullet");
		if (collision.gameObject.tag == "Bullet")
		{
			AudioManager.Play(AudioClipName.AsteroidHit);
			Destroy(collision.gameObject);

			if (transform.localScale.x <= 0.5)
			{
				Destroy(gameObject);
			}
			else
			{
				Vector3 newScale = transform.localScale;
				newScale.x /= 2;
				newScale.y /= 2;
				transform.localScale = newScale;

				float newColliderRadius = GetComponent<CircleCollider2D>().radius;
				newColliderRadius /= 2;
				GetComponent<CircleCollider2D>().radius = newColliderRadius;

				float angle = Random.Range(0, 2 * Mathf.PI);

				Vector3 asteroidLocation = transform.position;

				GameObject firstSplitAsteroidGameObject = Instantiate(gameObject, asteroidLocation, Quaternion.identity) as GameObject;
				Asteroid firstAsteroid = firstSplitAsteroidGameObject.GetComponent<Asteroid>();
				firstAsteroid.StartMoving(angle);

				GameObject secondSplitAsteroidGameObject = Instantiate(gameObject, asteroidLocation, Quaternion.identity) as GameObject;
				Asteroid secondAsteroid = secondSplitAsteroidGameObject.GetComponent<Asteroid>();
				secondAsteroid.StartMoving(angle);

				Destroy(gameObject);
			}
		}
		else
		{
			Destroy(collision.gameObject);
		}
	}
}
