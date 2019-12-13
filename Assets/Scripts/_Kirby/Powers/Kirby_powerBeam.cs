using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_powerBeam : NetworkBehaviour
{
    public Kirby_actor _kirby;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(!_kirby.isAlive) return;
        
        if (Input.GetKeyDown(KirbyConstants.KEY_SUCK)
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {
            activateBeam();
        }
    }

    public void activateBeam()
    {
        if (!_kirby.animator.GetBool(KirbyConstants.ANIM_TRIGGER_POWER_BEAM))
        {
            _kirby.isParalyzed = true;
            _kirby.animator.SetBool(KirbyConstants.ANIM_TRIGGER_POWER_BEAM, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_TRIGGER_POWER_BEAM, true, this.gameObject);
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyPowerBeam);
        }
    }

    public void finishBeamPower()
    {
        _kirby.isParalyzed = false;
        _kirby.animator.SetBool(KirbyConstants.ANIM_TRIGGER_POWER_BEAM, false);
        AudioPlayerSFXController.instance._audioSource.Stop();
    }
}
