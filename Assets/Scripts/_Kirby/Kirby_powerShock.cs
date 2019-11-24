using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerShock : MonoBehaviour
{
    private Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(Input.GetKey(KirbyConstants.KEY_SUCK) 
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {
            shockOn();
        } else {
            shockOff();
        }
    }

    public void shockOn()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, true);
    }

    public void shockOff()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false);
    }
}
