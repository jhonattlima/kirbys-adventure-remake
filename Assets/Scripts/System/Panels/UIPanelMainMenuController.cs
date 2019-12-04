using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMainMenuController : MonoBehaviour
{
    private bool lanMode;
    private GameObject[] buttonsMatch;

    public static UIPanelMainMenuController instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        buttonsMatch = PrefabsAndInstancesLibrary.instance.panelListOfMatchesButtonsList;
    }

    public void eventButtonClick(Button button)
    {
        switch(button.name)
        {
            case SystemConstants.BUTTON_NAME_LAN_MODE:
                lanMode = true;
                LanController.instance.listenMatches();
                PrefabsAndInstancesLibrary.instance.panelListOfMatches.SetActive(true);
                break;
            case SystemConstants.BUTTON_NAME_ONLINE_MODE:
                lanMode = false;
                PrefabsAndInstancesLibrary.instance.panelListOfMatches.SetActive(true);
                // TODO
                break;
            case SystemConstants.BUTTON_NAME_QUIT_MODE:
                Application.Quit();
                break;
            case SystemConstants.BUTTON_NAME_CREATE:
                if(string.IsNullOrEmpty(PrefabsAndInstancesLibrary.instance.panelListOfMatchesInputFieldMatchName.GetComponentInChildren<Text>().text)
                    || PrefabsAndInstancesLibrary.instance.panelListOfMatchesInputFieldMatchName.GetComponentInChildren<Text>().text.Contains("/"))
                {
                    PrefabsAndInstancesLibrary.instance.panelListTextWarningMessage.GetComponent<Text>().text = "Please insert a valid name";
                    PrefabsAndInstancesLibrary.instance.panelListTextWarningMessage.GetComponent<Text>().color = Color.red;
                }
                else
                {
                    if (lanMode) LanController.instance.createMatch(PrefabsAndInstancesLibrary.instance.panelListOfMatchesInputFieldMatchName.GetComponentInChildren<Text>().text);
                    else Debug.Log("Created remote match");
                    GameManager.instance.waitForMatch();
                }
                break;
            case SystemConstants.BUTTON_NAME_BACK:
                if (lanMode) LanController.instance.cancelLanDiscovery();
                clearMatchButtons();
                PrefabsAndInstancesLibrary.instance.panelMainMenu.SetActive(true);
                PrefabsAndInstancesLibrary.instance.panelListOfMatches.SetActive(false);
                break;
            case SystemConstants.BUTTON_NAME_MATCH:
                joinGame(button.GetComponent<ButtonMatchController>());
                Debug.Log("Pressed button match.");
                break;
        }
    }

    private void joinGame(ButtonMatchController button)
    {
        if (lanMode) LanController.instance.enterOnMatch(button);
        else RemoteController.instance.enterOnMatch(button);
    }

    private void clearMatchButtons()
    {
        for (int i = 0; i<SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING; i++)
        {
            buttonsMatch[i].gameObject.GetComponentInChildren<Text>().text = string.Empty;
            buttonsMatch[i].gameObject.SetActive(false);
        }
    }

    public void updateMatchButtons(StoredData[] storedDatas)
    {
        for(int i = 0; i<SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING; i ++)
        {
            if(storedDatas[i]  != null)
            {
                buttonsMatch[i].gameObject.GetComponentInChildren<Text>().text = storedDatas[i].data;
                buttonsMatch[i].GetComponent<ButtonMatchController>().lanMatch = storedDatas[i];
                buttonsMatch[i].gameObject.SetActive(true);
            } 
            else
            {
                buttonsMatch[i].gameObject.SetActive(false);
            }
        }
    }
}
