using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelKirbyStatusController : MonoBehaviour
{
    public static UIPanelKirbyStatusController instance;
    public Text LifeText;
    public Text PowerText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        setLife(KirbyConstants.PLAYER_HEALTH_POINTS);
        setPower((int)Powers.None);
    }

    public void setLife(int amount)
    {
        LifeText.text = "Lifes: X " + amount;
        if (amount == 1)
        {
            LifeText.color = Color.red;
        }
        else
        {
            LifeText.color = Color.blue;
        }
    }

    public void setPower(int power)
    {
        string chosenPower;
        switch (power)
        {
            case (int)Powers.Fire:
                chosenPower = "Fire";
                PowerText.color = Color.red;
                break;
            case (int)Powers.Shock:
                chosenPower = "Shock";
                PowerText.color = Color.yellow;
                break;
            case (int)Powers.Beam:
                chosenPower = "Beam";
                PowerText.color = Color.magenta;
                break;
            default:
                chosenPower = "None";
                PowerText.color = Color.white;
                break;
        }
        PowerText.text = "Power: " + chosenPower;
    }
}
