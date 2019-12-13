using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_powerFire : NetworkBehaviour
{
    public Kirby_actor _kirby;

    private void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(!_kirby.isAlive) return;
        
        if (Input.GetKey(KirbyConstants.KEY_SUCK)
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {
            fireOn();
        }
        else
        {
            fireOff();
        }
    }

    public void fireOn()
    {
        if(!_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE))
        {
            _kirby.isParalyzed = true;
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_FIRE, true, this.gameObject);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, true);
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyPowerFire);
        }
    }

    public void fireOff()
    {
        if(_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE))
        {
            _kirby.isParalyzed = false;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_FIRE, false, this.gameObject);
            AudioPlayerSFXController.instance._audioSource.Stop();
        }
    }
}
