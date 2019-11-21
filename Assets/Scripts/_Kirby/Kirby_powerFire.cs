using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerFire : MonoBehaviour
{
    public Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
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
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, true);
    }

    public void fireOff()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, false);
    }
}
