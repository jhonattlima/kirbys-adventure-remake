using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsAndInstancesLibrary : MonoBehaviour
{
    public GameObject airBall;
    public GameObject starPower;
    public GameObject starBullet;
    public GameObject scenarioOnePart1;
    public GameObject scenarioOnePart2;
    public GameObject scenarioOnePart3;

    public GameObject enemyHotHead;
    public GameObject enemySparky;
    public GameObject enemyWaddleDoo;

    public GameObject panelMainMenu;
    public GameObject panelListOfMatches;
    public GameObject[] panelListOfMatchesButtonsList;
    public GameObject panelListOfMatchesInputFieldMatchName;
    public GameObject panelListTextWarningMessage;
    public GameObject panelWaitingForAnotherPlayerToConnect;
    public GameObject panelNetworkErrorMessage;
    public GameObject panelKirbyStatus;
    public GameObject panelGameOver;

    public static PrefabsAndInstancesLibrary instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }
}
