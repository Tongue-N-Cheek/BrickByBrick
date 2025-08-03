using UnityEngine;
using UnityEngine.UI;

public class ButtonDislike : MonoBehaviour
{
    public Button button;

    public void Start()
	{
		button.onClick.AddListener(() =>
		{
			GameManager.Instance.InteractWithCurrentPost(-1);
			GameManager.Instance.Scroll();
        });
	}
}
