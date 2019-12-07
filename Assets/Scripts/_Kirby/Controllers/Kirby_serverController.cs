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

    public void changeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (!_kirby.isServer) CmdChangeBoolAnimationStatus(parameterName, newStatus, prefab);
    }

    [Command]
    public void CmdChangeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        execChangeBoolAnimationStatus(parameterName, newStatus, prefab);
        RpcChangeBoolAnimationStatus(parameterName, newStatus, prefab);
    }

    [ClientRpc]
    public void RpcChangeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        execChangeBoolAnimationStatus(parameterName, newStatus, prefab);
    }

    private void execChangeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (prefab.GetComponent<Kirby_actor>())
        {
            prefab.GetComponent<Kirby_actor>().animator.SetBool(parameterName, newStatus);
        }
        else
        {
            prefab.GetComponent<Enemy_actor>().animator.SetBool(parameterName, newStatus);
        }
    }

    public void changeTriggerAnimation(string parameterName, GameObject prefab)
    {
        if(isServer)
        {
            execChangeTriggerAnimation(parameterName, prefab);
            RpcChangeTriggerAnimation(parameterName, prefab);
        }
        else
        {
            CmdChangeTriggerAnimation(parameterName, prefab);
        }
    }

    [Command]
    public void CmdChangeTriggerAnimation(string parameterName, GameObject prefab)
    {
        RpcChangeTriggerAnimation(parameterName, prefab);
        execChangeTriggerAnimation(parameterName, prefab);
    }

    [ClientRpc]
    public void RpcChangeTriggerAnimation(string parameterName, GameObject prefab)
    {
        execChangeTriggerAnimation(parameterName, prefab);
    }

    private void execChangeTriggerAnimation(string parameterName, GameObject prefab)
    {
        if (prefab.GetComponent<Kirby_actor>())
        {
            prefab.GetComponent<Kirby_actor>().animator.SetTrigger(parameterName);
        }
        else
        {
            prefab.GetComponent<Enemy_actor>().animator.SetTrigger(parameterName);
        }
    }

    [Command]
    public void CmdSpawnStarPowerPrefab(GameObject kirby)
    {
        Kirby_actor _kirby = kirby.GetComponent<Kirby_actor>();
        Kirby_powerStar star = Instantiate(PrefabsAndInstancesLibrary.instance.starPower, _kirby.spotToDropStar.position, _kirby.spotToDropStar.rotation).GetComponent<Kirby_powerStar>();
        NetworkServer.Spawn(star.gameObject);
        star.power = _kirby.enemy_powerInMouth;
        star.setPushDirection(-_kirby.spotToDropStar.transform.forward);
    }

    [Command]
    public void CmdSpawnEnemyPrefab(GameObject enemy, GameObject spawnSpot)
    {
        spawnSpot.GetComponent<EnemySpawnerController>().isBecomingInstantiated = true;
        GameObject enemyInstantiated = Instantiate(enemy, spawnSpot.transform.position, Quaternion.identity);
        NetworkServer.Spawn(enemyInstantiated);
        spawnSpot.GetComponent<EnemySpawnerController>().enemyAlreadyInstantiated = enemyInstantiated;
        spawnSpot.GetComponent<EnemySpawnerController>().isBecomingInstantiated = false;
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
        if (kirbyThatHasDiedNumber.Equals(GameManager.instance.localPlayer.playerNumber))
        {
            Debug.Log("Kirby Server Controller: This LocalKirby is the one that lost.");
            GameManager.instance.wonTheGame = false;
        }
        else
        {
            Debug.Log("Kirby Server Controller: This LocalKirby is the one that won.");
            GameManager.instance.wonTheGame = true;
        }
        GameManager.instance.gameOver();
    }

    [Command]
    public void CmdDealDamageToMob(GameObject mob, int damage)
    {
        RpcDealDamageToMob(mob, damage);
    }

    [ClientRpc]
    public void RpcDealDamageToMob(GameObject mob, int damage)
    {
        mob.GetComponent<Enemy_healthController>().takeDamage(damage);
    }
}
