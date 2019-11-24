using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour
{
    private bool lanMode;
    public void eventButtonClick(Button button)
    {
        switch(button.name)
        {
            case SystemConstants.BUTTON_NAME_LAN_MODE:
                lanMode = true;
                // TODO
                startGame();
                break;
            case SystemConstants.BUTTON_NAME_ONLINE_MODE:
                lanMode = false;
                // TODO
                break;
            case SystemConstants.BUTTON_NAME_QUIT_MODE:
                Application.Quit();
                break;
            case SystemConstants.BUTTON_NAME_CREATE_GAME:
                break;
            case SystemConstants.BUTTON_NAME_JOIN_GAME:
                break;
            case SystemConstants.BUTTON_NAME_BACK:
                break;

        }
    }

    private void startGame()
    {
        PrefabsLibrary.instance.canvasKirbyStatus.SetActive(true);
        PrefabsLibrary.instance.scenarioOne.SetActive(true);
        gameObject.SetActive(false);
        NetworkController.instance.startNewOnlineMatch();
    }
}
