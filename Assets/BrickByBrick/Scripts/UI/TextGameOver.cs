using TMPro;
using UnityEngine;

public class TextGameOver : MonoBehaviour
{
	public TextMeshProUGUI text;

	public void Start()
	{
		text.text = GameManager.Instance.GetGameOverText();
	}
}
