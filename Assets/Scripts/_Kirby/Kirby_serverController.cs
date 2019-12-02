using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_serverController : NetworkBehaviour
{
    private Kirby_actor _kirby;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    [Command]
    public void CmdStartGame()
    {
        RpcStartGame();
    }

    [ClientRpc]
    public void RpcStartGame()
    {
        GameManager.instance.startMatch();
    }

    [Command]
    public void CmdGameOverByDeath(int kirbyThatHasDiedNumber)
    {
        RpcGameOverByDeath(kirbyThatHasDiedNumber);
    }

    [ClientRpc]
    public void RpcGameOverByDeath(int kirbyThatHasDiedNumber)
    {
        Debug.Log("Entered on RPC");
        if(kirbyThatHasDiedNumber.Equals(GameManager.instance.localPlayer.playerNumber))
        {
            Debug.Log("Entered on Id is equals");
            GameManager.instance.wonTheGame = false;
        } else {
            Debug.Log("Entered on Id is not equals");
            GameManager.instance.wonTheGame = true;
        }
        GameManager.instance.gameOver();
    }
}
