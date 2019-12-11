using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelCharacterSelect : MonoBehaviour
{
    public void buttonSelect(Button button)
    {
        switch(button.name)
        {
            case SystemConstants.BUTTON_NAME_CHARACTER_BLUE:
                GameManager.instance.selectedKirbyType = Enum_kirbyTypes.blue;
                break;
            case SystemConstants.BUTTON_NAME_CHARACTER_PINK:
                GameManager.instance.selectedKirbyType = Enum_kirbyTypes.pink;
                break;
        }
        if (UIPanelMainMenuController.instance.lanMode) LanController.instance.createMatch(PrefabsAndInstancesLibrary.instance.panelListOfMatchesInputFieldMatchName.GetComponentInChildren<Text>().text);
        else RemoteController.instance.createMatch(PrefabsAndInstancesLibrary.instance.panelListOfMatchesInputFieldMatchName.GetComponentInChildren<Text>().text);
        GameManager.instance.waitForMatch();
    }
}
