using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField]
	GameObject prefabAsteroid;

    // Start is called before the first frame update
    void Start()
	{
		// get asteroid collider radius
		GameObject tempAsteroid = Instantiate<GameObject>(prefabAsteroid);
		CircleCollider2D asteroidCollider = tempAsteroid.GetComponent<CircleCollider2D>();
		float asteroidRadius = asteroidCollider.radius;
		Destroy(tempAsteroid);

		// save screen utils for efficiency
		float spawnYScreenCenter = (ScreenUtils.ScreenTop + ScreenUtils.ScreenBottom) / 2;
		float spawnXScreenCenter = (ScreenUtils.ScreenRight + ScreenUtils.ScreenLeft) / 2;

		Vector3 asteroidLocation;

		foreach (Direction direction in Enum.GetValues(typeof(Direction)))
		{
			switch (direction)
			{
				case Direction.Up:
					asteroidLocation = new Vector3(spawnXScreenCenter + asteroidRadius, ScreenUtils.ScreenBottom, 0);
					break;
				case Direction.Left:
					asteroidLocation = new Vector3(ScreenUtils.ScreenRight, spawnYScreenCenter - asteroidRadius, 0);
					break;
				case Direction.Down:
					asteroidLocation = new Vector3(spawnXScreenCenter + asteroidRadius, ScreenUtils.ScreenTop, 0);
					break;
				case Direction.Right:
					asteroidLocation = new Vector3(ScreenUtils.ScreenLeft, spawnYScreenCenter - asteroidRadius, 0);
					break;
				default: asteroidLocation = new Vector3(0, 0, 0);
					Debug.Log("Asteroid location is not identified");
					break;
			}
		GameObject asteroidGameObject = Instantiate(prefabAsteroid, asteroidLocation, Quaternion.identity) as GameObject;
		Asteroid asteroid = asteroidGameObject.GetComponent<Asteroid>();

		asteroid.Initialize(direction, asteroidLocation);
		}
	}
}
