using UnityEngine;
using UnityEngine.UI;

public class ButtonMessages : MonoBehaviour
{
	public Button button;
	public Image notificationImage;

	public void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.OpenMessages());
	}

	public void ShowNotification()
	{
		notificationImage.enabled = true;
	}

	public void HideNotification()
	{
		notificationImage.enabled = false;
	}
}
