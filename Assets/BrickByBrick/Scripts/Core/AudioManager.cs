using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager
{
	private static AudioMixer mixer;

	public static void Init(AudioMixer mixer) => AudioManager.mixer = mixer;

	public static void SetVolume(AudioGroupName group, float volume)
	{
		string key = GetStringAudioGroup(group);
		mixer.SetFloat(key, GetAttenuation(volume));
		PrefsManager.SetFloat(key, volume);
	}

	public static string GetStringAudioGroup(AudioGroupName group) => group switch
	{
		AudioGroupName.Master => "VolumeMaster",
		AudioGroupName.Music => "VolumeMusic",
		AudioGroupName.SFX => "VolumeSFX",
		_ => throw new System.Exception("Unknown audio group")
	};

	private static float GetAttenuation(float volume)
	{
		if (volume <= 0) return -80;
		return Mathf.Log10(volume) * 20;
	}
}

public enum AudioGroupName
{
	Master,
	Music,
	SFX
}
