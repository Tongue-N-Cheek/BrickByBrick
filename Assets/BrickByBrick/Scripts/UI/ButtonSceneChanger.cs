using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneChanger : MonoBehaviour
{
	public Button button;
	public string sceneName;

	public void Start()
	{
		button.onClick.AddListener(() => SceneManager.GoToScene(sceneName));
	}
}
