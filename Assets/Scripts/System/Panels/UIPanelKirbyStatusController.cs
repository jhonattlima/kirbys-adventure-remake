using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelKirbyStatusController : MonoBehaviour
{
    public static UIPanelKirbyStatusController instance;
    public Sprite[] spritesLifePointsPink;
    public Sprite[] spritesLifePointsBlue;
    public Sprite[] spritesPowers;
    public Text namePink;
    public Image lifeBarPink;
    public Image powerIconPink;
    public Text nameBlue;
    public Image lifeBarBlue;
    public Image powerIconBlue;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        setLife(KirbyConstants.PLAYER_HEALTH_POINTS, Enum_kirbyTypes.pink);
        setLife(KirbyConstants.PLAYER_HEALTH_POINTS, Enum_kirbyTypes.blue);
        setPower(Powers.None, Enum_kirbyTypes.pink);
        setPower(Powers.None, Enum_kirbyTypes.blue);
    }

    public void setLife(int amount, Enum_kirbyTypes kirbyType)
    {
        Image chosenImage = kirbyType.Equals(Enum_kirbyTypes.pink) ? lifeBarPink : lifeBarBlue;
        Sprite[] chosenCollection = kirbyType.Equals(Enum_kirbyTypes.pink) ? spritesLifePointsPink : spritesLifePointsBlue;
        chosenImage.sprite = chosenCollection[amount];
    }

    public void setPower(Powers power, Enum_kirbyTypes kirbyType)
    {
        Image chosenImage = kirbyType.Equals(Enum_kirbyTypes.pink) ? powerIconPink : powerIconBlue;
        switch (power)
        {
            case Powers.Fire:
                chosenImage.sprite = spritesPowers[1];
                break;
            case Powers.Shock:
                chosenImage.sprite = spritesPowers[2];
                break;
            case Powers.Beam:
                chosenImage.sprite = spritesPowers[3];
                break;
            default:
                chosenImage.sprite = spritesPowers[0];
                break;
        }
    }

    public void setName(string name, Enum_kirbyTypes kirbyType)
    {
        Debug.Log("Name received to set " + name);
        Text chosenText = kirbyType.Equals(Enum_kirbyTypes.pink) ? namePink : nameBlue;
        chosenText.text = name;
    }
}
