using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelGameOverController : MonoBehaviour
{
    public GameObject gameOverYouWin;
    public GameObject gameOverYouLose;

    private void OnEnable()
    {
        if (GameManager.instance.wonTheGame)
        {
            Debug.Log("Congrats! You win!!!");
            gameOverYouWin.SetActive(true);
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouwin);
        }
        else
        {
            Debug.Log("Game over! You lose!!!");
            gameOverYouLose.SetActive(true);
            AudioPlayerMusicController.instance.play(AudioPlayerMusicController.instance.gameOverYouLost);
        }
    }
}
