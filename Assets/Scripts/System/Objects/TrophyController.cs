using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            Kirby_actor kirby_Actor = other.GetComponent<Kirby_actor>();
            if(!kirby_Actor.isLocalPlayer) return;
            if(kirby_Actor.playerNumber == 1) kirby_Actor.kirbyServerController.CmdGameOverByDeath(2);
            else kirby_Actor.kirbyServerController.CmdGameOverByDeath(1);
        }
    }
}
