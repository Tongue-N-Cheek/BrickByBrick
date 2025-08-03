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
    public BossDialogue[] bossDialogues;
    [field: SerializeField]
    public GameState GameState { get; private set; }
    [SerializeField, Header("Audio")]
    private AudioMixer mixer;

    private PostConstructor postConstructor;
    private Image postTimerImage;
    private MessagesPanel messagesPanel;
    private BrainrotMeter brainrotMeter;

    private BossStage bossStage = BossStage.Tutorial;
    private bool isPaused = false;
    private float maxPostTimer = 10f;
    private float postTimer = 10f;
    private bool isTimerRunning = false;
    private Coroutine timerCoroutine;
    private Dictionary<Tags, int> repostedTags = new();
    private bool isBossPost = false;
    private int bossScore = 0;
    private GameOverReason gameOverReason = GameOverReason.None;

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
    public void SetBrainrotMeter(BrainrotMeter brainrotMeter) => this.brainrotMeter = brainrotMeter;

    public void Scroll()
    {
        postConstructor.Scroll();

        Post post = postConstructor.GetCurrentPostObject().Post;
        isBossPost = post.isBossPost;
        if (isBossPost)
        {
            postConstructor.DisableButtons();
            postConstructor.ShowNotification();
        }

        postTimer = maxPostTimer;
        postTimerImage.fillAmount = 1f;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        isTimerRunning = !post.pausesTime;
        if (isTimerRunning) timerCoroutine = StartCoroutine(PostTimer());

        messagesPanel.HidePanel();
    }

    public void EndOfPosts()
    {
        Debug.Log("End of posts");
    }

    public void InteractWithCurrentPost(int weightChange)
    {
        Post post = postConstructor.GetCurrentPostObject().Post;
        Algorithm.Interact(post.postTags.ToList(), weightChange);
        if (weightChange > 0) brainrotMeter.ChangeSanity(post.sanityChange);
    }

    public void Repost()
    {
        Post post = postConstructor.GetCurrentPostObject().Post;
        foreach (Tags tag in post.postTags)
        {
            if (!repostedTags.ContainsKey(tag))
            {
                repostedTags[tag] = 0;
            }
            repostedTags[tag]++;
        }
    }

    public void OpenMessages()
    {
        if (!isBossPost) return;
        messagesPanel.ShowPanel();
        postConstructor.HideNotification();
        messagesPanel.StartDialogue(bossDialogues[(int)bossStage - 1]); // Bad hardcoding
    }

    public void OpenComments()
    {
        // TODO: Unhide comments UI
    }

    public void ChangeScore(int scoreChange)
    {
        bossScore += scoreChange;
        Debug.LogWarning("Score: " + bossScore);
    }

    public bool WonAgainstBoss() => bossStage switch
    {
        BossStage.ExGirlfriend => bossScore >= 0,
        BossStage.CryptoBro => bossScore >= 2,
        BossStage.CEO => bossScore >= 4,
        _ => false,
    };

    public void AdvanceStage()
    {
        if (bossStage != BossStage.Tutorial && !WonAgainstBoss())
        {
            switch (bossStage)
            {
                case BossStage.ExGirlfriend:
                    gameOverReason = GameOverReason.ExGirlfriend;
                    break;
                case BossStage.CryptoBro:
                    gameOverReason = GameOverReason.CryptoBro;
                    break;
                case BossStage.CEO:
                    gameOverReason = GameOverReason.CEO;
                    break;
            }
            SceneManager.OverlayScene("S_GameOver");
            return;
        }

        if (bossStage == BossStage.CEO)
        {
            gameOverReason = GameOverReason.Win;
            SceneManager.OverlayScene("S_GameOver");
            return;
        }

        bossStage++;
        bossScore = 0;
        repostedTags.Clear();
        postConstructor.EnableButtons();

        List<Post> posts = Algorithm.GetPosts(bossStage);
        foreach (Post post in posts)
        {
            postConstructor.BuildPost(post);
        }
        postConstructor.BuildPost(bossPosts[(int)bossStage - 1]); // Bad hardcoding
    }

    public bool RepostedMinimum(Tags tag, int minimumReposts) =>
       (repostedTags.ContainsKey(tag) && repostedTags[tag] >= minimumReposts) || minimumReposts == 0;

    public void SetBrainrotted()
    {
        gameOverReason = GameOverReason.Brainrot;
        SceneManager.OverlayScene("S_GameOver");
    }

    public string GetGameOverText() => gameOverReason switch
    {
        GameOverReason.ExGirlfriend => "At first, you thought that this might work out. Despite the abrupt ending to your previous relationship, you genuinely thought that you would be able to make it work the second time. Alas, those were the notions of a naive version of yourself, one that still had hope of a brighter future. Not that you are sad or anything, on the contrary, you feel nothing at all. You put in all that effort, and for what? Perhaps one day you might find love, but right now, the only thing you can feel is hollow.",
        GameOverReason.CryptoBro => "While scrolling on your phone all day is fun and all, it doesn't pay the bills. Instead of getting a job like most normal people, you tried to take a more unique approach. Why put effort into making money when you can profit from someone else's? That philosophy has held true for centuries, unfortunately, you weren't quite able to pull it off. Not only did your attempt to overthrow Zach Nation fail, but you were also scammed in the process. Now you find yourself working a dead end job, barely making enough money to survive. All your hopes and dreams amounted to nothing but steps for the successful to walk upon.",
        GameOverReason.CEO => "You came so close, the entirety of Looker was in the palm of your hand, but you let it slip. Now you watch as someone else takes the place of CEO while you watch in frustration. With your goal unachievable, you are left to ponder what is left for you to do. Regardless of your choice, you are faced with one unavoidable truth. No matter what you do, no matter who you become, you will never achieve your dream. You are fated to fade into obscurity, and neither your name or legacy will live on after you pass. All that is left for you is to live what meager life you can, and do your best to forget your failure.",
        GameOverReason.Brainrot => "It started as just a five minute break, taking a look and whatever crawled its way into your feed. Five minutes then turned into ten, then twenty, then an hour. Eventually you lost track of time, but you also found that you didn't care. What is time but a currency used to scroll through your feed? You find that you can't even put your phone down anymore, even if you wanted to. Fortunately, you are perfectly content to continue scrolling, slowly rotting your brain away for countless years to come.",
        GameOverReason.Win => "Congratulations. You've come so far. You got your girlfriend back, you scammed a crypto bro out of their money, and you overthrew the CEO. You are a true hero. Now, watch as others attempt to take your place.",
        _ => "",
    };

    private void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        AudioManager.Init(mixer);

#if UNITY_EDITOR
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "S_Game")
        {
            Play();
        }
#endif
    }

    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(0f);
        Algorithm.Init(AllPosts.ToList());
        brainrotMeter.ResetSanity();
        GameState = GameState.Playing;
        bossStage = BossStage.Tutorial;
        gameOverReason = GameOverReason.None;
        postTimer = maxPostTimer;

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
    Tutorial, // Ignore, not really used for anything, but don't remove because it will break stuff
    ExGirlfriend,
    CryptoBro,
    CEO
}

public enum GameOverReason
{
    None,
    ExGirlfriend,
    CryptoBro,
    CEO,
    Brainrot,
    Win
}
