﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerController : NetworkBehaviour
{
    public GameObject enemyInstantiated;

    private void OnBecameVisible() 
    {

        Debug.Log("EnteredHere");
        if(!enemyInstantiated && isServer)
        {
            enemyInstantiated = Instantiate(PrefabsAndInstancesLibrary.instance.enemySparky, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemyInstantiated);
        }
    }
}
