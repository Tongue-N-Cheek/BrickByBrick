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
    public Post[] tutorialPosts;
    public Post[] bossPosts;
    [field: SerializeField]
    public GameState GameState { get; private set; }
    [SerializeField, Header("Audio")]
    private AudioMixer mixer;

    private PostConstructor postConstructor;
    private Image postTimerImage;
    private MessagesPanel messagesPanel;

    private BossStage bossStage = BossStage.Tutorial;
    private bool isPaused = false;
    private float maxPostTimer = 10f;
    private float postTimer = 10f;
    private bool isTimerRunning = false;
    private Coroutine timerCoroutine;
    private HashSet<Tags> repostedTags = new();

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
        if (GameState == GameState.Playing && postTimerImage != null && isTimerRunning)
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
    public void SetMessagesPanel(MessagesPanel messagesPanel) => this.messagesPanel = messagesPanel;

    public void Scroll()
    {
        postConstructor.Scroll();
        postTimer = maxPostTimer;
        postTimerImage.fillAmount = 1f;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        isTimerRunning = !postConstructor.GetCurrentPostObject().Post.pausesTime;
        if (isTimerRunning) timerCoroutine = StartCoroutine(PostTimer());
    }

    public void EndOfPosts()
    {
        Debug.Log("End of posts");
    }

    public void InteractWithCurrentPost(int weightChange)
    {
        Post post = postConstructor.GetCurrentPostObject().Post;
        Algorithm.Interact(post.postTags.ToList(), weightChange);
    }

    public void Repost()
    {
        Post post = postConstructor.GetCurrentPostObject().Post;
        repostedTags.UnionWith(post.postTags);
    }

    public void ToggleMessages()
    {
        messagesPanel.TogglePanel();
    }

    public void OpenComments()
    {
        // TODO: Unhide comments UI
    }

    public void AdvanceStage()
    {
        if (bossStage == BossStage.CEO) throw new System.Exception("End of game");
        bossStage++;

        List<Post> posts = Algorithm.GetPosts(bossStage);
        foreach (Post post in posts)
        {
            postConstructor.BuildPost(post);
        }
        postConstructor.BuildPost(bossPosts[0]);
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
        bossStage = BossStage.Tutorial;
        postTimer = maxPostTimer;
        // timerCoroutine = StartCoroutine(PostTimer());

        foreach (Post post in tutorialPosts)
        {
            postConstructor.BuildPost(post);
        }

        AdvanceStage();
    }

    private IEnumerator PostTimer()
    {
        yield return new WaitForSeconds(maxPostTimer);
        InteractWithCurrentPost(1);
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
    Tutorial,
    ExGirlfriend,
    CryptoBro,
    CEO
}
