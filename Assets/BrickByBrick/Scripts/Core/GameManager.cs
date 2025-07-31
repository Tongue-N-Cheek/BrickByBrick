using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField, Header("Audio")]
    private AudioMixer mixer;

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

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        AudioManager.Init(mixer);
    }
}
