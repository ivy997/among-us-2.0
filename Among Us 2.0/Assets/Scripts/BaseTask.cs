using System.Collections;
using UnityEngine;

public class BaseTask : MonoBehaviour
{
	protected IEnumerator TurnOffParentObject()
	{
		yield return new WaitForSeconds(2);
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}
