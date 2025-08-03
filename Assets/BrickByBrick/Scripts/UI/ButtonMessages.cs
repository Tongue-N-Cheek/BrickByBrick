using UnityEngine;
using UnityEngine.UI;

public class ButtonMessages : MonoBehaviour
{
	public Button button;
	public Image notificationImage;
	public AudioClip notificationSound;

	public void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.OpenMessages());
	}

	public void ShowNotification()
	{
		notificationImage.enabled = true;
		AudioManager.PlayOnce(notificationSound);
	}

	public void HideNotification()
	{
		notificationImage.enabled = false;
	}
}
