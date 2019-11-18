using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerBeam : MonoBehaviour
{
    public Kirby_actor _kirby;

    // Start is called before the first frame update
    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KirbyConstants.KEY_SUCK) 
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {
            beamOn();
        } else {
            beamOff();
        }
    }

    public void beamOn()
    {
        _kirby.isParalyzed = true;
        _kirby.areaOfBeamPower.SetActive(true);
    }

    public void beamOff()
    {
        _kirby.isParalyzed = false;
        _kirby.areaOfBeamPower.SetActive(false);
    }
}
