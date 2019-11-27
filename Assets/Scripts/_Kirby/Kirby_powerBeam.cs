using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_powerBeam : NetworkBehaviour 
{
    public Kirby_actor _kirby;

    // Start is called before the first frame update
    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(!isLocalPlayer) return;

        if(Input.GetKeyDown(KirbyConstants.KEY_SUCK) 
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {   
            // _kirby.serverKirbyController.CmdActivateBeam();  
            activateBeam();
        }
    }

    public void activateBeam()
    {
        _kirby.animator.SetTrigger(KirbyConstants.ANIM_TRIGGER_POWER_BEAM);
    }
}
