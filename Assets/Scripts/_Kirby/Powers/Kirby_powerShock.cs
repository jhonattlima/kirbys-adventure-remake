﻿using System.Collections;
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
        _kirby.isParalyzed = true;
        if (!_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK))
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SHOCK, true, this.gameObject);
        }
    }

    public void shockOff()
    {
        _kirby.isParalyzed = false;
        if (_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK))
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false, this.gameObject);
        }
    }
}
