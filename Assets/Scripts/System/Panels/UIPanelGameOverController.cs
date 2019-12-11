using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelGameOverController : MonoBehaviour
{
    public Sprite gameOverYouWin;
    public Sprite gameOverYouLose;
    public Image backgroundImage;

    private void OnEnable()
    {
        if (GameManager.instance.wonTheGame)
        {
            Debug.Log("Congrats! You win!!!");
            backgroundImage.sprite = gameOverYouWin;
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouwin);
        }
        else
        {
            Debug.Log("Game over! You lose!!!");
            backgroundImage.sprite = gameOverYouLose;
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouLost);
        }
    }
}
