using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
	private float fromTargetY;
	private float toTargetY;
	private float targetX;
	private Vector2 newTarget;

	void Start()
	{
		fromTargetY = GameObject.Find("AsteroidTargetLocationFrom").GetComponent<RectTransform>().transform.localPosition.y;
		toTargetY = GameObject.Find("AsteroidTargetLocationTo").GetComponent<RectTransform>().transform.localPosition.y;
		targetX = GameObject.Find("AsteroidTargetLocationTo").GetComponent<RectTransform>().transform.localPosition.x;
		newTarget = new Vector2(targetX, Random.Range(fromTargetY, toTargetY));
	}

	void FixedUpdate()
	{
		this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, newTarget, 6);
		if (this.transform.localPosition.x == newTarget.x)
		{
			Destroy(gameObject);
		}
	}
}
