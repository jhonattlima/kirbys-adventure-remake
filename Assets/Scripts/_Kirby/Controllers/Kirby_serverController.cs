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
        PrefabsAndInstancesLibrary.instance.canvasKirbyStatus.SetActive(true);
        PrefabsAndInstancesLibrary.instance.panelMainMenu.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelListOfMatches.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelWaitingForAnotherPlayerToConnect.SetActive(false);
        AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.stageVegetableValley);
        GameManager.instance.listOfPlayers = GameObject.FindGameObjectsWithTag(KirbyConstants.TAG_PLAYER);

        Enum_kirbyTypes kirbyTypesPlayer1 = Enum_kirbyTypes.pink;
        foreach (var player in GameManager.instance.listOfPlayers)
        {
            Kirby_actor actor = player.GetComponent<Kirby_actor>();
            if (actor.playerNumber == 1) kirbyTypesPlayer1 = actor.kirbyType;
        }

        foreach (var player in GameManager.instance.listOfPlayers)
        {
            Kirby_actor actor = player.GetComponent<Kirby_actor>();
            if (actor.playerNumber == 2)
            {
                actor.kirbyType = kirbyTypesPlayer1.Equals(Enum_kirbyTypes.pink) ? Enum_kirbyTypes.blue : Enum_kirbyTypes.pink;
            }
            UIPanelKirbyStatusController.instance.setName(actor.playerName, actor.kirbyType);
            GameObject arrow =  actor.kirbyType.Equals(Enum_kirbyTypes.pink)? actor.arrowPink : actor.arrowBlue;
            arrow.SetActive(true);
            actor.isParalyzed = false;
        }
    }

    public void changeBoolAnimationStatus(string parameterName, bool newStatus, GameObject prefab)
    {
        if (!_kirby.isAlive) return;
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
    public void CmdSpawnEnemyPrefab(GameObject enemySpawner)
    {
        EnemySpawnerController enemySpawnerController = enemySpawner.GetComponent<EnemySpawnerController>();
        GameObject enemyInstantiated = Instantiate(enemySpawnerController.enemyToBeInstantiated, enemySpawner.transform.position, Quaternion.identity);
        NetworkServer.Spawn(enemyInstantiated);
        enemySpawnerController.enemyAlreadyInstantiated = enemyInstantiated;
        enemySpawnerController.isBecomingInstantiated = false;
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
        execGameOver(kirbyThatHasDiedNumber);
    }

    [ClientRpc]
    public void RpcGameOverByDeath(int kirbyThatHasDiedNumber)
    {
        execGameOver(kirbyThatHasDiedNumber);
    }

    private void execGameOver(int kirbyThatHasDiedNumber)
    {
        Debug.Log("LocalPlayer is " + GameManager.instance.localPlayer.playerNumber);
        Debug.Log("Won the game is " + GameManager.instance.wonTheGame);
        if (kirbyThatHasDiedNumber == GameManager.instance.localPlayer.playerNumber) GameManager.instance.wonTheGame = false;
        else GameManager.instance.wonTheGame = true;
        GameManager.instance.gameOverDisconnection = true;
        GameManager.instance.gameOver();
    }

    [Command]
    public void CmdDealDamageToMob(GameObject mob, int damage)
    {
        mob.GetComponent<Enemy_healthController>().takeDamage(damage);
    }

    public void playAtPoint(GameObject playPoint, string sfxName)
    {
        AudioPlayerSFXController.instance.playAtPoint(playPoint, sfxName);
        if (isServer) RpcPlaySfx(playPoint, sfxName);
        else CmdPlaySfx(playPoint, sfxName);
    }

    [Command]
    public void CmdPlaySfx(GameObject playPoint, string sfxName)
    {
        AudioPlayerSFXController.instance.playAtPoint(playPoint, sfxName);
    }

    [ClientRpc]
    public void RpcPlaySfx(GameObject playPoint, string sfxName)
    {
        AudioPlayerSFXController.instance.playAtPoint(playPoint, sfxName);
    }

    [Command]
    public void CmdSetPlayerInfo(GameObject kirby, int playerNumber, Enum_kirbyTypes kirbyType, string playerName)
    {
        Kirby_actor actor = kirby.GetComponent<Kirby_actor>();
        actor.playerNumber = playerNumber;
        actor.kirbyType = kirbyType;
        actor.playerName = playerName;
        if (actor.kirbyType == Enum_kirbyTypes.blue)
        {
            CmdSetPlayerShaderToBlue(kirby);
        }
    }

    [Command]
    public void CmdSetPlayerShaderToBlue(GameObject kirby)
    {
        // execChangeShadersToBlue(kirby);
        // RpcSetPlayerShadertoBlue(kirby);
    }

    [ClientRpc]
    public void RpcSetPlayerShadertoBlue(GameObject kirby)
    {
        execChangeShadersToBlue(kirby);
    }

    private void execChangeShadersToBlue(GameObject kirby)
    {
        Kirby_actor actor = kirby.GetComponent<Kirby_actor>();
        // if(actor.kirbyType == Enum_kirbyTypes.blue)
        // {
        //     actor.bodyMaterialsController.kirbyEyes.SetTexture("_MainTex", PrefabsAndInstancesLibrary.instance.kirbyBlueEye);
        //     actor.bodyMaterialsController.kirbyMouth.SetTexture("_MainTex", PrefabsAndInstancesLibrary.instance.kirbyBlueMouth);
        //     actor.bodyMaterialsController.kirbyFoot.SetTexture("_MainTex", PrefabsAndInstancesLibrary.instance.kirbyBlueFoot);
        // }
    }

    [Command]
    public void CmdSetKirbyLife(int healthPoints, Enum_kirbyTypes kirbyType)
    {
        execSetKirbyLife(healthPoints, kirbyType);
        RpcSetKirbyLife(healthPoints, kirbyType);
    }

    [ClientRpc]
    public void RpcSetKirbyLife(int healthPoints, Enum_kirbyTypes kirbyType)
    {
        execSetKirbyLife(healthPoints, kirbyType);
    }

    private void execSetKirbyLife(int healthPoints, Enum_kirbyTypes kirbyType)
    {
        UIPanelKirbyStatusController.instance.setLife(healthPoints, kirbyType);
    }


    [Command]
    public void CmdSetKirbyPower(Powers power, Enum_kirbyTypes kirbyType)
    {
        execSetKirbyPower(power, kirbyType);
        RpcSetKirbyPower(power, kirbyType);
    }

    [ClientRpc]
    public void RpcSetKirbyPower(Powers power, Enum_kirbyTypes kirbyType)
    {
        execSetKirbyPower(power, kirbyType);
    }

    private void execSetKirbyPower(Powers power, Enum_kirbyTypes kirbyType)
    {
        UIPanelKirbyStatusController.instance.setPower(power, kirbyType);
    }

}