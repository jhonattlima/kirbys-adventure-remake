using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_actor : NetworkBehaviour
{
    public Enemy_healthController healthController;
    public CharacterController characterController;
    public Animator animator;
    public int type;
    public int touchDamage;
    public bool isKirbyClose = false;

    void Awake()
    {
        healthController = GetComponent<Enemy_healthController>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
}
