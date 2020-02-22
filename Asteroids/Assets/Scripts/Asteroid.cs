using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
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
		// apply impulse force to get game object moving
		const float MinImpulseForce = 2f;
		const float MaxImpulseForce = 7f;
		const float baseAngleUp = 75 * Mathf.PI / 180;
		const float baseAngleLeft = 165 * Mathf.PI / 180;
		const float baseAngleDown = 255 * Mathf.PI / 180;
		const float baseAngleRight = 345 * Mathf.PI / 180;
		float angle = Random.Range(0, Mathf.PI / 6);
		switch (direction)
		{
			case Direction.Up:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleUp + angle), Mathf.Sin(baseAngleUp + angle));
				break;
			case Direction.Left:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleLeft + angle), Mathf.Sin(baseAngleLeft + angle));
				break;
			case Direction.Down:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleDown + angle), Mathf.Sin(baseAngleDown + angle));
				break;
			case Direction.Right:
				moveDirection = new Vector2(
					Mathf.Cos(baseAngleRight + angle), Mathf.Sin(baseAngleRight + angle));
				break;
		}

		transform.position = asteroidPosition;

		float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
		GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
	}
}
