using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject enemyWaddleDee;
    public GameObject enemyBrontoBurt;
    public GameObject enemyPoppyBrosJr;

    public GameObject panelMainMenu;
    public GameObject panelListOfMatches;
    public GameObject[] panelListOfMatchesButtonsList;
    public Text panelListOfMatchesInputFieldMatchName;
    public Text panelListOfMatchesInputFieldPlayerName;
    public Text panelListTextWarningMessage;
    public GameObject panelWaitingForAnotherPlayerToConnect;
    public GameObject panelNetworkErrorMessage;
    public GameObject panelCharacterSelect;
    public GameObject canvasKirbyStatus;
    public GameObject panelGameOver;

    public static PrefabsAndInstancesLibrary instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }
}
