using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_actor : MonoBehaviour
{
    public Enemy_healthController healthController;
    public Animator animator;
    public int type;
    public int touchDamage;
    public bool isKirbyClose = false;

    private void Awake() {
        healthController = GetComponent<Enemy_healthController>();
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        other?.gameObject?.GetComponent<Kirby_healthController>()?.takeDamage(touchDamage);
    }
}
