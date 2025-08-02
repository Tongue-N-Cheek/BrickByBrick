using UnityEngine;

public class MessagesPanel : MonoBehaviour
{
	public CanvasGroup canvasGroup;

	public void Start()
	{
		GameManager.Instance.SetMessagesPanel(this);
		canvasGroup.alpha = 0f;
	}

	public void OnDestroy()
	{
		GameManager.Instance.SetMessagesPanel(null);
	}

	public void ShowMessage(string message)
	{
		// TODO
	}

	public void ShowPanel()
	{
		canvasGroup.alpha = 1f;
	}

	public void HidePanel()
	{
		canvasGroup.alpha = 0f;
	}

	public void TogglePanel()
	{
		canvasGroup.alpha = canvasGroup.alpha < 0.5f ? 1f : 0f;
	}
}
