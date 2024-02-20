using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource audio_Source;

    [Header("Player Sound")]
    public AudioClip slipVoice;
    public AudioClip hitVoice;
    public AudioClip dropOcean;
    public AudioClip gameLose;

    public int hitVoiceCount;
  
    void Start()
    {
        audio_Source = GetComponent<AudioSource>();
    }

   public void playHitVoice()
    {
        if (GameManager.Instance.gameData.soundON)
        {
            if (hitVoiceCount == 0)
            {
                hitVoiceCount++;
                audio_Source.PlayOneShot(hitVoice);
            }
        }      
    }

    public void playDropOcean()
    {
        if (GameManager.Instance.gameData.soundON)
        {
            audio_Source.PlayOneShot(dropOcean);
        }
    }
    public void playGameLose()
    {
        if (GameManager.Instance.gameData.soundON)
        {
            audio_Source.PlayOneShot(gameLose);
        }
    }
}
