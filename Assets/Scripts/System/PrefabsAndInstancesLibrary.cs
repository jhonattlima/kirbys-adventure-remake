using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabsAndInstancesLibrary : MonoBehaviour
{
    public GameObject starPower;
    public GameObject scenarioOnePart1;
    public GameObject panelMainMenu;
    public GameObject panelListOfMatches;
    public GameObject[] panelListOfMatchesButtonsList;
    public GameObject panelWaitingForAnotherPlayerToConnect;
    public GameObject panelNetworkErrorMessage;
    public GameObject panelCharacterSelect;
    public GameObject canvasKirbyStatus;
    public GameObject panelGameOver;
    public Text panelListOfMatchesInputFieldMatchName;
    public Text panelListOfMatchesInputFieldPlayerName;
    public Text panelListTextWarningMessage;

    public static PrefabsAndInstancesLibrary instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }
}
