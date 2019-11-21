using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_actor : MonoBehaviour
{
    public int type;
    public Enemy_healthController healthController;

    public int touchDamage = 0;

    private void Awake() {
        healthController = GetComponent<Enemy_healthController>();
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        other?.gameObject?.GetComponent<Kirby_healthController>()?.takeDamage(touchDamage);
    }
}
