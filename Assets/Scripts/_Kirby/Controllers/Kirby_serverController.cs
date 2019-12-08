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
        prepareGameToStart();
        RpcStartGame();
    }

    [ClientRpc]
    public void RpcStartGame()
    {
        prepareGameToStart();
    }

    private void prepareGameToStart()
    {
        PrefabsAndInstancesLibrary.instance.panelKirbyStatus.SetActive(true);
        PrefabsAndInstancesLibrary.instance.panelMainMenu.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelWaitingForAnotherPlayerToConnect.SetActive(false);
        AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.stageVegetableValley);
        GameManager.instance.listOfPlayers = GameObject.FindGameObjectsWithTag(KirbyConstants.TAG_PLAYER);
        GameManager.instance.localPlayer.isParalyzed = false;
    }

    public void changeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (!_kirby.isServer) CmdChangeBoolAnimationStatus(parameterName, newStatus, prefab);
        else RpcChangeBoolAnimationStatus(parameterName, newStatus, prefab);
    }

    [Command]
    public void CmdChangeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (prefab.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            if (prefab.GetComponent<Kirby_actor>().animator.GetBool(parameterName) != newStatus) prefab.GetComponent<Kirby_actor>().animator.SetBool(parameterName, newStatus);
        }
        else
        {
            if (prefab.GetComponent<Enemy_actor>().animator.GetBool(parameterName) != newStatus) prefab.GetComponent<Enemy_actor>().animator.SetBool(parameterName, newStatus);
        }
        RpcChangeBoolAnimationStatus(parameterName, newStatus, prefab);
    }

    [ClientRpc]
    public void RpcChangeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (prefab.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            // Debug.Log("Is kirbyActor here? " + prefab.GetComponent<Kirby_actor>().name);
            // Debug.Log("Is animator here? " + prefab.GetComponent<Kirby_actor>().animator.name);
            // Debug.Log("Which parameter was used? " + parameterName);
            // Debug.Log("Which status was used? " + newStatus);

            if (prefab.GetComponent<Kirby_actor>().animator.GetBool(parameterName) != newStatus) prefab.GetComponent<Kirby_actor>().animator.SetBool(parameterName, newStatus);
        }
        else
        {
            if (prefab.GetComponent<Enemy_actor>().animator.GetBool(parameterName) != newStatus) prefab.GetComponent<Enemy_actor>().animator.SetBool(parameterName, newStatus);
        }
    }

    [Command]
    public void CmdGetDamaged(int amountOfDamage, GameObject prefab)
    {
        if (prefab.GetComponent<Kirby_actor>()) prefab.GetComponent<Kirby_healthController>().takeDamage(amountOfDamage);
        else prefab.GetComponent<Enemy_healthController>().takeDamage(amountOfDamage);
    }

    [Command]
    public void CmdSpawnStarPowerPrefab(GameObject kirby)
    {
        Kirby_actor _kirby = kirby.GetComponent<Kirby_actor>();
        Debug.Log("Entered on spawn star power");
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
    public void CmdDestroyPrefab(GameObject prefab)
    {
        Destroy(prefab);
    }

    [Command]
    public void CmdSpawnStarBulletPrefab(GameObject kirby, bool isLookingRight)
    {
        Kirby_actor actor = kirby.GetComponent<Kirby_actor>();
        Kirby_powerStarBullet starBullet = Instantiate(actor.starBulletPrefab, kirby.transform.position, kirby.transform.rotation).GetComponent<Kirby_powerStarBullet>();
        starBullet._kirby = actor;
        starBullet.setBulletDirection(isLookingRight ? actor.directionRight : actor.directionLeft);
        actor.isFullOfEnemy = false;
        actor.enemy_powerInMouth = (int)Powers.None;
    }

    [ClientRpc]
    public void RpcSpawnStarBulletPrefab(GameObject kirby, bool isLookingRight)
    {
        Kirby_actor actor = kirby.GetComponent<Kirby_actor>();
        Kirby_powerStarBullet starBullet = Instantiate(actor.starBulletPrefab, kirby.transform.position, kirby.transform.rotation).GetComponent<Kirby_powerStarBullet>();
        starBullet._kirby = actor;
        starBullet.setBulletDirection(isLookingRight ? actor.directionRight : actor.directionLeft);
        actor.isFullOfEnemy = false;
        actor.enemy_powerInMouth = (int)Powers.None;
    }

    [Command]
    public void CmdGameOverByDeath(int kirbyThatHasDiedNumber)
    {
        RpcGameOverByDeath(kirbyThatHasDiedNumber);
    }

    [ClientRpc]
    public void RpcGameOverByDeath(int kirbyThatHasDiedNumber)
    {
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
        mob.GetComponent<Enemy_healthController>().takeDamage(damage);
    }
}
