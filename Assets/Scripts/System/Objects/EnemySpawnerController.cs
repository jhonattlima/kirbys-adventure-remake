using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerController : NetworkBehaviour
{
    public GameObject enemyToBeInstantiated; 
    [SyncVar]
    public GameObject enemyAlreadyInstantiated = null;
    [SyncVar]
    public bool isBecomingInstantiated = false;

    // private void OnBecameVisible()
    // {
    //     if (!enemyAlreadyInstantiated)
    //     {
    //         // Debug.Log("EnemySpawnerController: No enemy instantiated. Will do now...");
    //         if(isBecomingInstantiated) return;
    //         isBecomingInstantiated = true;
    //         GameManager.instance.localPlayer.GetComponent<Kirby_actor>().kirbyServerController.CmdSpawnEnemyPrefab(this.gameObject);
    //     }
    // }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(!isServer) return;
        if (!enemyAlreadyInstantiated && other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            isBecomingInstantiated = true;
            GameObject enemyInstantiated = Instantiate(enemyToBeInstantiated, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemyInstantiated);
            enemyAlreadyInstantiated = enemyInstantiated;
            isBecomingInstantiated = false;
            // Debug.Log("EnemySpawnerController: No enemy instantiated. Will do now...");
            // if(isBecomingInstantiated) return;
            // isBecomingInstantiated = true;
            // GameManager.instance.localPlayer.GetComponent<Kirby_actor>().kirbyServerController.CmdSpawnEnemyPrefab(this.gameObject);
        }
    }
}
