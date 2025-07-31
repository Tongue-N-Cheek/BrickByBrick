using UnityEngine;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour
{
	public Button button;

	public void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.Quit());
	}
}
