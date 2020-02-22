using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	Sprite asteroid_red;
	[SerializeField]
	Sprite asteroid_blue;
	[SerializeField]
	Sprite asteroid_grey;

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

		// apply impulse force to get game object moving
		const float MinImpulseForce = 2f;
		const float MaxImpulseForce = 7f;
		float angle = Random.Range(0, 2 * Mathf.PI);
		Vector2 direction = new Vector2(
			Mathf.Cos(angle), Mathf.Sin(angle));
		float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
		GetComponent<Rigidbody2D>().AddForce(
			direction * magnitude,
			ForceMode2D.Impulse);
	}
}
