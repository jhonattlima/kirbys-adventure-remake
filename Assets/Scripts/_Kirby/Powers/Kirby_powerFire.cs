﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_powerFire : NetworkBehaviour
{
    public Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(!isLocalPlayer) return;
        if(Input.GetKey(KirbyConstants.KEY_SUCK) 
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {
            fireOn();
        } else {
            fireOff();
        }
    }

    public void fireOn()
    {
        _kirby.isParalyzed = true;
        //_kirby.fireArea.SetActive(true);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, true);
    }

    public void fireOff()
    {
        _kirby.isParalyzed = false;
        //_kirby.fireArea.SetActive(false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, false);
    }
}
