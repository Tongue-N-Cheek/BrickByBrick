using UnityEngine;
using UnityEngine.UI;

public class ButtonScroll : MonoBehaviour
{
	public Button button;

	public void Start()
	{
		button.onClick.AddListener(() =>
		{
			GameManager.Instance.Repost();
			GameManager.Instance.InteractWithCurrentPost(2);
			GameManager.Instance.Scroll();
		});
	}
}
