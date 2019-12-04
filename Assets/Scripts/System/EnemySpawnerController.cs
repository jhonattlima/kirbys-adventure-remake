using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerController : NetworkBehaviour
{
    public GameObject enemyInstantiated;

    private void OnBecameVisible() 
    {
        if(!enemyInstantiated && isServer)
        {
            //enemyInstantiated = Instantiate(PrefabsAndInstancesLibrary.instance.enemyHotHead, transform.position, Quaternion.identity);
            //NetworkServer.Spawn(enemyInstantiated);
        }
    }
}
