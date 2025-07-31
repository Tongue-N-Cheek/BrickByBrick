using UnityEngine;

public static class PrefsManager
{
	public static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);
	public static float GetFloat(string key, float? defaultValue) =>
		PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue ?? 0f;
}
