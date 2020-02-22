using UnityEngine;

public class ScreenWarper : MonoBehaviour
{

	// screen wrapping support
	public static float colliderRadius;

	// Start is called before the first frame update
	void Start()
    {
		//saved for efficiency 
		colliderRadius = GetComponent<CircleCollider2D>().radius;
	}
	
	/// <summary>
	/// Called when the game object becomes invisible to the camera
	/// </summary>
	void OnBecameInvisible()
	{
		Vector2 position = transform.position;

		// check left, right, top, and bottom sides
		if (position.x + colliderRadius < ScreenUtils.ScreenLeft ||
		    position.x - colliderRadius > ScreenUtils.ScreenRight)
		{
			position.x *= -1;
		}
		if (position.y - colliderRadius > ScreenUtils.ScreenTop ||
		    position.y + colliderRadius < ScreenUtils.ScreenBottom)
		{
			position.y *= -1;
		}

		// move ship
		transform.position = position;
	}
}
