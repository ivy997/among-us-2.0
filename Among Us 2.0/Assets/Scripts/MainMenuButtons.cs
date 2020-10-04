using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
	public GameObject overlay;
	public GameObject[] menus;
	public GameObject[] options;
	private GameObject prevMenu;
	private GameObject currMenu;
	private Stack<GameObject> menusStack = new Stack<GameObject>();

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TurnOnPreviousMenu();

			if (options.Any(x => x.activeInHierarchy))
			{
				ClosePopWindow();
			}
		}
	}

	public void OnCircleButtonsClick(string nameOfButton)
	{
		options.First(x => x.name == nameOfButton).SetActive(true);
		overlay.SetActive(true);
	}

	public void OnMenusClick(string nameOfMenu)
	{
		prevMenu = menus.First(x => x.activeInHierarchy == true);
		menusStack.Push(prevMenu);
		prevMenu.SetActive(false);

		currMenu = menus.First(x => x.name == nameOfMenu);
		menusStack.Push(currMenu);
		currMenu.SetActive(true);

	}

	public void OnBackClick()
	{
		TurnOnPreviousMenu();
	}

	public void OnFreePlayClick()
	{
		SceneManager.LoadScene(1);
	}

	public void OnExitClick()
	{
		Application.Quit();
	}

	public void OnCloseClick()
	{
		ClosePopWindow();
	}

	private void ClosePopWindow()
	{
		float closeAnimationTime = PlayAnimationAndGetLength();
		StartCoroutine(WaitAndTurnOffOverlay(closeAnimationTime));
	}

	private void TurnOnPreviousMenu()
	{
		if (menusStack.Count > 1)
		{
			currMenu = menusStack.Pop();
			currMenu.SetActive(false);

			prevMenu = menusStack.Peek();
			prevMenu.SetActive(true);
		}
	}

	private float PlayAnimationAndGetLength()
	{
		Animator animator = overlay.GetComponent<Animator>();
		animator.Play("MenuOverlayClose");
		return animator.runtimeAnimatorController.animationClips[1].length;
	}

	private IEnumerator WaitAndTurnOffOverlay(float animationTime)
	{
		yield return new WaitForSeconds(animationTime);
		options.First(x => x.activeInHierarchy).SetActive(false);
		overlay.SetActive(false);
	}
}
