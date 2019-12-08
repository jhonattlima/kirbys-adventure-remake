using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelGameOverController : MonoBehaviour
{
    public Image gameOverYouWin;
    public Image gameOverYouLose;
    public Text gameoverMessage;

    private void OnEnable()
    {
        if (GameManager.instance.wonTheGame)
        {
            gameoverMessage.text = "Congrats! You win!!!";
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouwin);
        }
        else
        {
            gameoverMessage.text = "Game over! You lose!!!";
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouLost);
        }
    }
}
