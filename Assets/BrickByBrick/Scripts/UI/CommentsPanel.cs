using TMPro;
using UnityEngine;

public class CommentsPanel : MonoBehaviour
{
	[SerializeField, Header("Components")]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private TextMeshProUGUI comment1;
	[SerializeField]
	private TextMeshProUGUI comment2;
	[SerializeField]
	private TextMeshProUGUI comment3;

	public bool IsOpen => canvasGroup.alpha > 0f;

	public void Start()
	{
		GameManager.Instance.SetCommentsPanel(this);
		canvasGroup.alpha = 0f;
	}

	public void OpenComments(string[] comments)
	{
		canvasGroup.alpha = 1f;
		if (comments.Length >= 1) comment1.text = comments[0];
		if (comments.Length >= 2) comment1.text = comments[1];
		if (comments.Length >= 3) comment1.text = comments[2];
	}

	public void CloseComments() => canvasGroup.alpha = 0f;
}
