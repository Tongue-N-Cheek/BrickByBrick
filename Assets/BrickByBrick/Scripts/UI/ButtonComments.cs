using UnityEngine;
using UnityEngine.UI;

public class ButtonComments : MonoBehaviour
{
	public Button button;

	public void Start()
	{
		button.onClick.AddListener(() =>
		{
			GameManager.Instance.OpenComments();
		});
	}
}
