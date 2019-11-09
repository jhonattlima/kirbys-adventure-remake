using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_actor : MonoBehaviour
{
    public CapsuleCollider suckArea;
    public SphereCollider mouth;
    public CharacterController characterController;
    public bool hasPower;
    void Awake()
    {
        suckArea = GetComponentInChildren<Kirby_markerSuckArea>().GetComponent<CapsuleCollider>();
        mouth = GetComponentInChildren<Kirby_mouthController>().GetComponent<SphereCollider>();
        characterController = GetComponent<CharacterController>();
    }
}
