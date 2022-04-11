using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;     

    public AudioSource jumpAudio, colourChangeAudio, starCollectAudio, defeatAudio;

    private void Awake()
    {
        instance = this; // Sets instance of AudioManager Singleton
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
