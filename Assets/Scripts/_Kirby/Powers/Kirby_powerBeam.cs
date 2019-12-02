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
        if(!isLocalPlayer) return;

        if(Input.GetKeyDown(KirbyConstants.KEY_SUCK) 
        && _kirby.characterController.isGrounded
        && !_kirby.isFullOfAir)
        {    
            activateBeam();
        }
    }

    public void activateBeam()
    {
        _kirby.animator.SetTrigger(KirbyConstants.ANIM_TRIGGER_POWER_BEAM);
        StartCoroutine(waitForAnimationToFinish());
    }

    IEnumerator waitForAnimationToFinish()
    {
        _kirby.isParalyzed = true;
        yield return new WaitForSeconds(_kirby.animator.GetCurrentAnimatorStateInfo(0).length);
        _kirby.isParalyzed = false;
    }
}
