using UnityEngine;
using UnityEngine.UI;

public class ImagePostTimer : MonoBehaviour
{
	public Image postTimerImage;

	public void Awake()
	{
		GameManager.Instance.SetPostTimerImage(postTimerImage);
	}

	public void Oestroy()
	{
		GameManager.Instance.SetPostTimerImage(null);		
	}
}
