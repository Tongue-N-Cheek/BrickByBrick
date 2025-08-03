using UnityEngine;
using UnityEngine.UI;

public class BrainrotMeter : MonoBehaviour
{
	public Image image;

	[SerializeField, Header("Sanity")]
	private int maxSanity = 9;
	[SerializeField]
	private int currentSanity = 9;

	public void Start()
	{
		GameManager.Instance.SetBrainrotMeter(this);
	}

	public void ChangeSanity(int change)
	{
		currentSanity = Mathf.Clamp(currentSanity + change, 0, maxSanity);
		image.fillAmount = (float)currentSanity / maxSanity;

		if (currentSanity <= 0)
		{
			GameManager.Instance.SetBrainrotted();
		}
	}

	public void ResetSanity()
	{
		currentSanity = maxSanity;
		image.fillAmount = 1f;
	}
}
