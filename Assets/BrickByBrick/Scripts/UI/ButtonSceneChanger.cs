using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneChanger : MonoBehaviour
{
	public Button button;
	public string sceneName;

	public virtual void Start()
	{
		button.onClick.AddListener(() => GameManager.Instance.GoToScene(sceneName));
	}
}
