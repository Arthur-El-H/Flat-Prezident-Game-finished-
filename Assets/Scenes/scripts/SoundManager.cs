using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundscape;
    public AudioSource soundtrack;
    bool sound = true;
    void Start()
    {
        soundtrack.loop = true;
    }

    public void toggleSound()
    {
        if (sound)
        {
            soundtrack.Stop();
            soundscape.Stop();
            sound = false;
        }

        else
        {
            soundtrack.Play();
            soundscape.Play();
            soundscape.loop = true;
            soundtrack.loop = true;
            sound = true;
        }
    }

    public void startSound()
    {
        soundtrack.loop = true;
        soundscape.loop = true;
        soundscape.Play();
    }

}
