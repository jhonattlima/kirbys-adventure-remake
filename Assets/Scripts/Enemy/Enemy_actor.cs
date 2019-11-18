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

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Kirby Triggered");
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Kirby Collided");
        if(other.gameObject.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            other.gameObject.GetComponent<Kirby_healthController>().takeDamage(touchDamage);
        }
    }
}
