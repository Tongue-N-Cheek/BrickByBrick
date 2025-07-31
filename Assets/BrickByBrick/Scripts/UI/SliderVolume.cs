using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
	public Slider slider;
	public AudioGroupName group;

	public void Start()
	{
		slider.value = PrefsManager.GetFloat(AudioManager.GetStringAudioGroup(group), 1f);
		slider.onValueChanged.AddListener(value => AudioManager.SetVolume(group, value));
		AudioManager.SetVolume(group, slider.value);
	}
}
