using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_actor : NetworkBehaviour
{
    public SphereCollider mouth;
    public BoxCollider areaOfSucking;
    public Animator animator;
    public Kirby_serverController kirbyServerController;
    public CharacterController characterController;
    public Kirby_powerFire powerFire;
    public GameObject fireArea;
    public Kirby_powerBeam powerBeam;
    public Kirby_powerShock powerShock;
    public GameObject shockArea;
    public GameObject airPrefab;
    public GameObject starPrefab;
    public GameObject starBulletPrefab;
    public Transform spotToDropStar;
    public List<SkinnedMeshRenderer> body;
    public Transform currentArea;
    public int enemy_powerInMouth = (int)Powers.None;
    public bool hasPower = false;
    public bool isLookingRight = true;
    public bool isFullOfEnemy = false;
    public bool isFullOfAir = false;
    public bool isSucking = false;
    public bool isParalyzed = false;
    public bool isInvulnerable = false;
    public float cooldownAction = 0;

    public Enum_kirbyTypes kirbyType;

    [SyncVar]
    public int playerNumber;

    public Vector3 directionRight
    {
        get
        {
            return currentArea.right;
        }
    }
    public Vector3 directionForward
    {
        get
        {
            return currentArea.forward;
        }
    }
    public Vector3 directionBack
    {
        get
        {
            return -currentArea.forward;
        }
    }
    public Vector3 directionLeft
    {
        get
        {
            return -currentArea.right;
        }
    }

    void Awake()
    {
        currentArea = PrefabsAndInstancesLibrary.instance.scenarioOnePart1.transform;
        transform.Rotate(0, 90f, 0);
        powerFire = GetComponent<Kirby_powerFire>();
        powerShock = GetComponent<Kirby_powerShock>();
        powerBeam = GetComponent<Kirby_powerBeam>();
    }

    void Start()
    {
        if (!isLocalPlayer) return;
        CameraController.instance.localKirby = this;
        playerNumber = 1;
        GameManager.instance.localPlayer = this;
        GameManager.instance.localPlayerServerController = kirbyServerController;
        if (isLocalPlayer && !isServer) // If it is player 2
        {
            kirbyServerController.CmdStartGame();
        }
    }

    void Update()
    {
        if (cooldownAction > 0)
        {
            cooldownAction -= Time.deltaTime;
        }
    }
}

