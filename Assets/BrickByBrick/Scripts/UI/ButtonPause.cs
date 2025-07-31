using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
	public Button button;

	public void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.TogglePause());
	}
}
