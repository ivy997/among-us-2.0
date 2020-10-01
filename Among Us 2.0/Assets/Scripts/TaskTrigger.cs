using System.Collections;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
	public GameObject parent;
	public GameObject task;
	private bool playerInRange;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
		{
			if (parent.activeInHierarchy)
			{
				StartCoroutine(TurnOffParent());
				return;
			}

			if (task.activeInHierarchy)
			{
				task.SetActive(false);
				return;
			}

			parent.SetActive(true);
			task.SetActive(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerInRange = false;
		}
	}

	private IEnumerator TurnOffParent()
	{
		Animator animator = parent.GetComponent<Animator>();
		animator.Play("CloseTask");
		yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[1].length);
		parent.SetActive(false);
	}
}
