using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField]
    public GameState GameState { get; private set; }
    [SerializeField, Header("Audio")]
    private AudioMixer mixer;

    private PostConstructor postConstructor;
    [SerializeField]
    private Post tempPostData;

    private bool isPaused = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public void GoToScene(string sceneName)
    {
        GameState = GameState.Menu;
        Unpause();
        SceneManager.GoToScene(sceneName);
    }
#pragma warning restore CS0618 // Type or member is obsolete

    public void Play()
    {
        StartCoroutine(DelayedPlay());
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        GameState = GameState.Paused;
        Time.timeScale = 0f;
        SceneManager.OverlayScene("S_Pause");
    }

    public void Unpause()
    {
        isPaused = false;
        GameState = GameState.Playing;
        Time.timeScale = 1f;
        SceneManager.RemoveOverlayScene("S_Pause");
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void SetPostConstructor(PostConstructor postConstructor) => this.postConstructor = postConstructor;

    public void Scroll()
    {
        postConstructor.Scroll();
    }

    private void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        AudioManager.Init(mixer);

#if UNITY_EDITOR
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "S_Game")
        {
            StartCoroutine(DelayedPlay());
        }
#endif
    }

    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(0f);
        GameState = GameState.Playing;
        postConstructor.BuildPost(tempPostData);
        postConstructor.BuildPost(tempPostData);
        // TODO: start the algorithm
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}
