using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_actor : MonoBehaviour
{
    public CapsuleCollider suckArea;
    public SphereCollider mouth;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        suckArea = GetComponentInChildren<Kirby_markerSuckArea>().GetComponent<CapsuleCollider>();
        mouth = GetComponentInChildren<Kirby_markerMouth>().GetComponent<SphereCollider>();
        characterController = GetComponent<CharacterController>();
    }
}
