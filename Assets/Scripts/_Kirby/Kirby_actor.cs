using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_actor : MonoBehaviour
{
    public SphereCollider mouth;
    public CapsuleCollider areaOfSucking;
    public GameObject areaOfFirePower;
    public GameObject areaOfShockPower;
    public GameObject areaOfBeamPower;
    public CharacterController characterController;
    public Kirby_powerFire powerFire;
    public Kirby_powerBeam powerBeam;
    public Kirby_powerShock powerShock;
    public bool hasPower = false;
    public bool isLookingRight = true;
    public bool isFullOfEnemy = false;
    public bool isFullOfAir = false;
    public bool isSucking = false;
    public bool isParalyzed = false;
    public int enemy_powerInMouth = (int)Powers.None;
    public GameObject airPrefab;
    public Transform currentArea;
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
        mouth = GetComponentInChildren<Kirby_mouthController>().GetComponent<SphereCollider>();
        areaOfSucking = GetComponentInChildren<Kirby_SuckAreaController>().GetComponent<CapsuleCollider>();

        powerFire = GetComponent<Kirby_powerFire>();
        areaOfFirePower = GetComponentInChildren<Kirby_powerFireArea>().gameObject;
        areaOfFirePower.SetActive(false);
    
        powerShock = GetComponent<Kirby_powerShock>();
        areaOfShockPower = GetComponentInChildren<Kirby_powerShockArea>().gameObject;
        areaOfShockPower.SetActive(false);

        powerBeam = GetComponent<Kirby_powerBeam>();
        areaOfBeamPower = GetComponentInChildren<Kirby_powerBeamArea>().gameObject;
        areaOfBeamPower.SetActive(false);
    }
}
