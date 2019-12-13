using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerController : NetworkBehaviour
{
    public GameObject enemyToBeInstantiated; 
    public GameObject enemyAlreadyInstantiated = null;
    public bool isBecomingInstantiated = false;
    public int playersInArea;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER)) playersInArea ++;
        if (!isBecomingInstantiated 
            && enemyAlreadyInstantiated == null 
            && other.CompareTag(KirbyConstants.TAG_PLAYER)
            && playersInArea == 1)
        {
            isBecomingInstantiated = true;
            GameObject enemyInstantiated = Instantiate(enemyToBeInstantiated, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemyInstantiated);
            enemyAlreadyInstantiated = enemyInstantiated;
            isBecomingInstantiated = false;
            //GameManager.instance.localPlayer.GetComponent<Kirby_actor>().kirbyServerController.CmdSpawnEnemyPrefab(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER)) playersInArea --;
    }
}
