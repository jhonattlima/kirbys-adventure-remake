using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_playerCloseController : MonoBehaviour
{
    public int numberOfPlayersClose = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            Debug.Log("Player entered on enemyArea.");
            numberOfPlayersClose ++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            Debug.Log("Player left on enemyArea.");
            numberOfPlayersClose --;
            if(numberOfPlayersClose <= 0) GameManager.instance.localPlayer.kirbyServerController.CmdDestroyPrefab(transform.parent.gameObject);
        }
    }
}
