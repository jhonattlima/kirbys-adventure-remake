using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_actor : MonoBehaviour
{
    public CapsuleCollider suckArea;
    public SphereCollider mouth;
    public CharacterController characterController;
    public bool hasPower = false;
    public bool isFullOfEnemy = false;
    public bool isFullOfAir = false;
    public bool isSucking = false;
    public Enemy_actor enemy_Actor = null;
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

    private void Awake() {
        suckArea = GetComponentInChildren<Kirby_SuckAreaController>().GetComponent<CapsuleCollider>();
        mouth = GetComponentInChildren<Kirby_mouthController>().GetComponent<SphereCollider>();
        characterController = GetComponent<CharacterController>();
    }
}
