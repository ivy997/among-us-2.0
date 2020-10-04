using UnityEngine;

public class Teleport : MonoBehaviour
{
	public bool playerInRange;
	public float yOffset;
	public GameObject player;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject vent2;
	public GameObject vent3;

	private Rigidbody2D playerRigidBody; 
	private new SpriteRenderer renderer;
	private PlayerMovement playerMovement;
	private bool actionKeyReleased;

	// Start is called before the first frame update
	void Start()
	{
		playerRigidBody = player.GetComponent<Rigidbody2D>();
		renderer = player.GetComponent<SpriteRenderer>();
		playerMovement = player.GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange)
		{
			OnClickVentEvent(playerRigidBody);
		}
		
		//If inside "If" block, this method doesn't work correctly
		ToggleArrowRenderer(renderer);
		
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			actionKeyReleased = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && actionKeyReleased)
		{
			OnClickArrowEvent(leftArrow, rightArrow, playerRigidBody);

			actionKeyReleased = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerInRange = false;
		}
	}

	private Vector2 PlayerPositionOnVent(Rigidbody2D player, GameObject vent)
	{
		return player.position = new Vector2(vent.transform.position.x, 
			vent.transform.position.y + yOffset);
	}

	private void TogglePlayerRenderer()
	{
		renderer.enabled = !renderer.enabled;
	}

	private void TogglePlayerMovement()
	{
		playerMovement.enabled = !playerMovement.enabled;
	}

	private void ToggleArrowRenderer(SpriteRenderer renderer)
	{
		SpriteRenderer leftArrowRenderer = leftArrow.GetComponent<SpriteRenderer>();
		SpriteRenderer rightArrowRenderer = rightArrow.GetComponent<SpriteRenderer>();

		if (renderer.enabled)
		{
			leftArrowRenderer.enabled = false;
			rightArrowRenderer.enabled = false;
		}
		else
		{
			leftArrowRenderer.enabled = true;
			rightArrowRenderer.enabled = true;
		}
	}

	private void OnClickArrowEvent(GameObject leftArrow, GameObject rightArrow, Rigidbody2D player)
	{
		RaycastHit2D hit = Raycast();

		if (hit.collider != null)
		{
			var selection = hit.transform;

			if (selection.position == leftArrow.transform.position)
			{
				player.position = PlayerPositionOnVent(player, vent2);
			}
			else if(selection.position == rightArrow.transform.position)
			{
				player.position = PlayerPositionOnVent(player, vent3);
			}
		}
	}

	private void OnClickVentEvent(Rigidbody2D player)
	{
		RaycastHit2D hit = Raycast();

		if (hit.collider != null)
		{
			var selection = hit.transform;

			if (selection.tag == "Vent")
			{
				EnterVentEvent();
			}
		}
	}

	private RaycastHit2D Raycast()
	{
		var ray = Camera.main.ScreenPointToRay(new Vector3
				(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

		return Physics2D.Raycast(ray.origin, ray.direction);
	}

	private void EnterVentEvent()
	{
		playerRigidBody.position = PlayerPositionOnVent(playerRigidBody, this.gameObject);

		TogglePlayerRenderer();

		TogglePlayerMovement();
	}
}
