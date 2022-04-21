using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    public static AudioManager instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioSource backgroundAudio, jumpAudio, colourChangeAudio, starCollectAudio, defeatAudio;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Sets instance of AudioManager Singleton
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject, 0f);
        }
    }

    private void Start()
    {
        PlayBackgroundAudio();
    }

    public void PlayBackgroundAudio()
    {
        backgroundAudio.Play();
    }

    public void PlayJumpAudio()
    {
        jumpAudio.Play();
    }

    public void PlayColourChangeAudio()
    {
        colourChangeAudio.Play();
    }

    public void PlayStarCollectAudio()
    {
        starCollectAudio.Play();
    }

    public void PlayDefeatAudio()
    {
        jumpAudio.Stop();
        defeatAudio.Play();
    }
}
