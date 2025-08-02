using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField, Header("Data")]
    public Post[] AllPosts { get; private set; }
    [field: SerializeField]
    public GameState GameState { get; private set; }
    [SerializeField, Header("Audio")]
    private AudioMixer mixer;

    private PostConstructor postConstructor;
    private Image postTimerImage;
    [SerializeField]
    private Post tempPostData;

    private BossStage bossStage = BossStage.ExGirlfriend;
    private bool isPaused = false;
    private float maxPostTimer = 10f;
    private float postTimer = 10f;
    private Coroutine timerCoroutine;

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

    public void Update()
    {
        if (GameState == GameState.Playing && postTimerImage != null)
        {
            postTimer -= Time.deltaTime;
            postTimerImage.fillAmount = postTimer / maxPostTimer;
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
    public void SetPostTimerImage(Image postTimerImage) => this.postTimerImage = postTimerImage;

    public void Scroll()
    {
        postConstructor.Scroll();
        postTimer = maxPostTimer;
        StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(PostTimer());
    }

    public void EndOfPosts()
    {
        Debug.Log("End of posts");
    }

    public void InteractWithCurrentPost()
    {
        Post post = postConstructor.GetCurrentPostObject().Post;
        Algorithm.Interact(post, 1);
    }

    private void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        AudioManager.Init(mixer);
        Algorithm.Init(AllPosts.ToList());

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
        postTimer = maxPostTimer;
        timerCoroutine = StartCoroutine(PostTimer());
        List<Post> posts = Algorithm.GetPosts(bossStage);
        foreach (Post post in posts)
        {
            postConstructor.BuildPost(post);
        }
    }

    private IEnumerator PostTimer()
    {
        yield return new WaitForSeconds(maxPostTimer);
        InteractWithCurrentPost();
        Scroll();
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}

public enum BossStage
{
    ExGirlfriend,
    CryptoBro,
    CEO
}
