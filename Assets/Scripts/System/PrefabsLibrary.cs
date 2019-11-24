using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsLibrary : MonoBehaviour
{
    public GameObject canvasMainMenu;
    public GameObject canvasKirbyStatus;
    public GameObject canvasGameOver;
    public GameObject kirby;
    public GameObject airBall;
    public GameObject starPower;
    public GameObject starBullet;
    public GameObject scenarioOne;

    public static PrefabsLibrary instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }
}
