using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayerSFXController : MonoBehaviour
{
    public static AudioPlayerSFXController instance;
    public AudioClip kirbyFly;
    public AudioClip kirbyJump;
    public AudioClip kirbyInhale;
    public AudioClip kirbySwallow;
    public AudioClip kirbyAbsorb;
    public AudioClip kirbyExpelStar;
    public AudioClip kirbyExpelAirBall;
    public AudioClip kirbyWalk;
    public const string SFX_NAME_KIRBY_FLOAT = "kirbyFloat";
    public const string SFX_NAME_KIRBY_JUMP = "kirbyJump";
    public const string SFX_NAME_KIRBY_INHALE = "kirbyInhale";
    public const string SFX_NAME_KIRBY_SWALLOW = "kirbySwallow";
    public const string SFX_NAME_KIRBY_ABSORB = "kirbyAbsorb";
    public const string SFX_NAME_KIRBY_EXPEL_STAR = "kirbyExpelStar";
    
    public AudioSource _audioSource;

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

    public void playAtPoint(GameObject point, string sfx)
    {
        AudioClip chosenClip = null;
        switch(sfx)
        {
            case ("kirbyFloat"):
                chosenClip = kirbyFly;
                break;
            case ("kirbyInhale"):
                chosenClip = kirbyInhale;
                break;
            case ("kirbyJump"):
                chosenClip = kirbyJump;
                break;
            case ("kirbySwallow"):
                chosenClip = kirbySwallow;
                break;
            case ("kirbyAbsorb"):
                chosenClip = kirbyAbsorb;
                break;
            case ("kirbyExpelStar"):
                chosenClip = kirbyExpelStar;
                break;
            case (""):
                chosenClip = kirbyFly;
                break;
        }
        AudioSource.PlayClipAtPoint(chosenClip, point.transform.position);
    }

    public void play(AudioClip sfx)
    {
        _audioSource.loop = false;
        _audioSource.clip = null;
        _audioSource.PlayOneShot(sfx);
    }

    public void playInLoop(AudioClip sfx)
    {
        _audioSource.loop = true;
        _audioSource.clip = sfx;
        _audioSource.Play();
    }
}
