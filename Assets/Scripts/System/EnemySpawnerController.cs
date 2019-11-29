using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerController : NetworkBehaviour
{
    public GameObject enemyInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        testing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void testing()
    {
        if (!isServer) return;
        if (!enemyInstantiated)
        {
            Debug.Log("EnteredHere");
            enemyInstantiated = Instantiate(PrefabsAndInstancesLibrary.instance.enemyHotHead, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemyInstantiated);
        }
    }

    void OnBecomeVisible()
    {
        if(!enemyInstantiated)
        {
            Debug.Log("EnteredHere");
            enemyInstantiated = Instantiate(PrefabsAndInstancesLibrary.instance.enemyHotHead, transform.position, Quaternion.identity);
            NetworkServer.Spawn(enemyInstantiated);
        }
    }
}
