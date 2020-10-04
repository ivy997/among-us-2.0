using System.Collections;
using UnityEngine;

public class TaskButtons : MonoBehaviour
{
	public GameObject parent;

	public void CloseParentTaskObject()
	{
		Animator animator = parent.GetComponent<Animator>();
		if (animator != null)
		{
			float timeOfAnimation = animator.runtimeAnimatorController.animationClips[1].length;
			animator.SetBool("open", false);
			StartCoroutine(TurnOffTasksAndParent(timeOfAnimation));
		}
	}

	private IEnumerator TurnOffTasksAndParent(float animationTime)
	{
		yield return new WaitForSeconds(animationTime);
		foreach (Transform child in parent.transform)
		{
			if (child.CompareTag("Task"))
			{
				child.gameObject.SetActive(false);
			}
		}

		parent.SetActive(false);
	}
}
