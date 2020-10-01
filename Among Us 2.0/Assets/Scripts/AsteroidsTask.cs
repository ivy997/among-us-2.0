using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AsteroidsTask : BaseTask, IPointerClickHandler
{
	public GameObject[] asteroids;
	public RectTransform spawnLocation;
	public RectTransform fromTargetLocationY;
	public RectTransform toTargetLocationY;
	public GameObject scope;
	public Text score;
	public int scoreToFinish;
	private int counter;
	private float currTime = 0;

	// Start is called before the first frame update
	public void Start()
	{
		scope.SetActive(false);
		currTime = Random.Range(0.65F, 2F);
		score.text = $"Destoyed: {counter}";
	}

	public void FixedUpdate()
	{
		ManageSpawnTimer();
		if (currTime <= 0)
		{
			RandomizeSpawnLocation();
			SpawnAsteroid();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		scope.SetActive(true);
		scope.transform.position = eventData.pressPosition;
		if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Asteroid"))
		{
			Destroy(eventData.pointerCurrentRaycast.gameObject);
			counter++;
			score.text = $"Destoyed: {counter}";
			if (counter >= scoreToFinish)
			{
				StartCoroutine(TurnOffParentObject());
				counter = 0;
			}
		}
	}

	private void SpawnAsteroid()
	{
		GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
		GameObject instance = Instantiate(asteroid, spawnLocation.transform.localPosition, transform.rotation) as GameObject;
		instance.transform.SetParent(this.transform, false);
	}

	private void RandomizeSpawnLocation()
	{
		spawnLocation.localPosition
			= new Vector2(spawnLocation.localPosition.x, Random.Range(fromTargetLocationY.localPosition.y, toTargetLocationY.localPosition.y));
	}

	private void ManageSpawnTimer()
	{
		if (currTime <= 0)
		{
			currTime = Random.Range(0.5F, 1.5F);
		}
		else
		{
			currTime -= Time.deltaTime;
		}
	}
}
