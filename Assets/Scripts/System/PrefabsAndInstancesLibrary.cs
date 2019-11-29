using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsAndInstancesLibrary : MonoBehaviour
{
    public GameObject canvasKirbyStatus;
    public GameObject canvasGameOver;
    public GameObject airBall;
    public GameObject starPower;
    public GameObject starBullet;
    public GameObject scenarioOnePart1;
    public GameObject scenarioOnePart2;
    public GameObject scenarioOnePart3;

    public GameObject canvasMainMenu;
    public GameObject panelMainMenu;

    public GameObject enemyHotHead;
    public GameObject enemySparky;
    public GameObject enemyWaddleDoo;

    public GameObject panelListOfMatches;
    public GameObject[] panelListOfMatchesButtonsList;
    public GameObject panelListOfMatchesInputFieldMatchName;
    public GameObject panelListTextWarningMessage;
    public GameObject panelWaitingForAnotherPlayerToConnect;

    public static PrefabsAndInstancesLibrary instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }
}
