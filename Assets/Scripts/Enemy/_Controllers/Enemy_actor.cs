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

    private void Awake()
    {
        healthController = GetComponent<Enemy_healthController>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Kirby_actor>())
        {
            other.gameObject.GetComponent<Kirby_healthController>().takeDamage(touchDamage);
            animator.SetTrigger(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE);
        }
    }
}
