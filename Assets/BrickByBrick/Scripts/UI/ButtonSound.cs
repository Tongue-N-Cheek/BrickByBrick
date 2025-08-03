using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonSound : MonoBehaviour
{
    public Button button;
    public AudioClip clickSound;

    public virtual void Start()
    {
        button.onClick.AddListener(() =>
        {
            AudioManager.PlayOnce(clickSound);
        });
    }

    public virtual void PlayButtonSound()
    {
        AudioManager.PlayOnce(clickSound);
    }
}
