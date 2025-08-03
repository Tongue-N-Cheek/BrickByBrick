using UnityEngine;
using UnityEngine.UI;

public class ImagePostTimer : MonoBehaviour
{
	public Image postTimerImage;

	public void Start()
	{
		GameManager.Instance.SetPostTimerImage(postTimerImage);
	}

	public void OnDestroy()
	{
		GameManager.Instance.SetPostTimerImage(null);		
	}
}
