using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_powerShock : NetworkBehaviour
{
    private Kirby_actor _kirby;

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
            shockOn();
        }
        else
        {
            shockOff();
        }
    }

    public void shockOn()
    {
        if (!_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK))
        {
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SHOCK, true, this.gameObject);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, true);
            _kirby.isParalyzed = true;
        }
    }

    public void shockOff()
    {
        if (_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK))
        {
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false, this.gameObject);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false);
            _kirby.isParalyzed = false;
        }
    }
}
