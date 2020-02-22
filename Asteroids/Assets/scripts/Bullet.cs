using UnityEngine;

public class Bullet : MonoBehaviour
{
	// timer support
	private const float bulletLifespanSeconds = 2;
	private Timer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
		deathTimer = gameObject.AddComponent<Timer>();
		deathTimer.Duration = bulletLifespanSeconds;
		deathTimer.Run();
	}

    // Update is called once per frame
    void Update()
    {
		// destroy bullet if death timer finished
		if (deathTimer.Finished)
		{
			Destroy(gameObject);
		}
	}

    public void ApplyForce(Vector2 directionOfTheForce)
    {
	    const float magnitude = 10f;
	    GetComponent<Rigidbody2D>().AddForce(directionOfTheForce * magnitude, ForceMode2D.Impulse);
	}
}
