using System;
using System.Collections.Generic;

public static class SceneManager
{
	private static List<string> scenes = new();

	[Obsolete("Use GameManager.GoToScene instead")]
	public static void GoToScene(string sceneName)
	{
		scenes.Clear();
		scenes.Add(sceneName);
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	public static void OverlayScene(string sceneName)
	{
		scenes.Add(sceneName);
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			sceneName,
			UnityEngine.SceneManagement.LoadSceneMode.Additive
		);
	}

	public static void RemoveOverlayScene(string sceneName)
	{
		scenes.Remove(sceneName);
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
	}
}
