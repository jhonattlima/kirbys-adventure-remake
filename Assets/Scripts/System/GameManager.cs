using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Kirby_actor localPlayer;
    public Kirby_serverController localPlayerServerController;
    public GameObject[] listOfPlayers;
    public bool wonTheGame;
    public bool gameOverDisconnection = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void waitForMatch()
    {
        PrefabsAndInstancesLibrary.instance.panelMainMenu.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelWaitingForAnotherPlayerToConnect.SetActive(true);
    }

    public void startMatch()
    {
        PrefabsAndInstancesLibrary.instance.panelMainMenu.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelWaitingForAnotherPlayerToConnect.SetActive(false);
        PrefabsAndInstancesLibrary.instance.panelKirbyStatus.SetActive(true);
        listOfPlayers = GameObject.FindGameObjectsWithTag(KirbyConstants.TAG_PLAYER);
    }

    public void restartMatch()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void gameOver()
    {
        gameOverDisconnection = true;
        localPlayer.GetComponent<Kirby_actor>().isParalyzed = true;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        localPlayer = null;
        StartCoroutine(setGameOverPanel());
    }

    IEnumerator setGameOverPanel()
    {
        PrefabsAndInstancesLibrary.instance.panelGameOver.SetActive(true);
        yield return new WaitForSeconds(5);
        PrefabsAndInstancesLibrary.instance.panelGameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        gameOverDisconnection = false;
    }

    public Transform figureOutClosestPlayer(Transform enemy)
    {
        listOfPlayers = GameObject.FindGameObjectsWithTag(KirbyConstants.TAG_PLAYER);
        if (listOfPlayers.Length < 2) return listOfPlayers[0].transform;
        return Vector3.Distance(listOfPlayers[0].transform.position, enemy.position) <
            Vector3.Distance(listOfPlayers[1].transform.position, enemy.position) ? listOfPlayers[0].transform : listOfPlayers[1].transform;
    }

    public void restartWithNetworkError()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        StartCoroutine(cShowNetworkError());
    }

    IEnumerator cShowNetworkError()
    {
        Debug.Log("Error: One of the players disconnected.");
        yield return new WaitForSeconds(1);

        PrefabsAndInstancesLibrary.instance.panelNetworkErrorMessage.SetActive(true);
        yield return new WaitForSeconds(SystemConstants.TIME_TO_SHOW_ERROR_PANEL);
        PrefabsAndInstancesLibrary.instance.panelNetworkErrorMessage.SetActive(false);
    }
}
