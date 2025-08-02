using UnityEngine;
using UnityEngine.UI;

public class ButtonMessages : MonoBehaviour
{
	public Button button;

	public void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.ToggleMessages());
	}
}
