using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_actor : MonoBehaviour
{
    public SphereCollider mouth;
    public CapsuleCollider areaOfSucking;
    public Animator animator;
    public Server_KirbyController serverKirbyController;
    public CharacterController characterController;
    public Kirby_powerFire powerFire;
    public Kirby_powerBeam powerBeam;
    public Kirby_powerShock powerShock;
    public GameObject airPrefab;
    public GameObject starPrefab;
    public GameObject starBulletPrefab;
    public Transform currentArea;
    public int enemy_powerInMouth = (int)Powers.None;
    public bool hasPower = false;
    public bool isLookingRight = true;
    public bool isFullOfEnemy = false;
    public bool isFullOfAir = false;
    public bool isSucking = false;
    public bool isParalyzed = false;
    public bool isInvulnerable = false;
    
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

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        serverKirbyController = GetComponent<Server_KirbyController>();
        mouth = GetComponentInChildren<Kirby_mouthController>().GetComponent<SphereCollider>();
        areaOfSucking = GetComponentInChildren<Kirby_SuckAreaController>().GetComponent<CapsuleCollider>();
        powerFire = GetComponent<Kirby_powerFire>();
        powerShock = GetComponent<Kirby_powerShock>();
        powerBeam = GetComponent<Kirby_powerBeam>();

        mouth.gameObject.SetActive(false);
        areaOfSucking.gameObject.SetActive(false);
    }
}
