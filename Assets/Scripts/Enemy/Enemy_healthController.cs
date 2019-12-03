using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_healthController : MonoBehaviour
{
    public int healthPoints;

    public void takeDamage(int damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            Debug.Log("X_X");
            Destroy(gameObject);
        }
    }
}
