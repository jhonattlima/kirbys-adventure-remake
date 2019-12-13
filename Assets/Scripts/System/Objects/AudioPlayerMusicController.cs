using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerMusicController : MonoBehaviour
{
    public static AudioPlayerMusicController instance;
    public AudioClip mainMenu;
    public AudioClip stageVegetableValley;
    public AudioClip gameOverYouwin;
    public AudioClip gameOverYouLost;

    AudioSource _audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void play (AudioClip music)
    {
        _audioSource.Stop();
        _audioSource.clip = music;
        _audioSource.Play();
    }
}
