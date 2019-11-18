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
        _kirby.isParalyzed = true;
        _kirby.areaOfFirePower.SetActive(true);
    }

    public void fireOff()
    {
        _kirby.isParalyzed = false;
        _kirby.areaOfFirePower.SetActive(false);
    }
}
