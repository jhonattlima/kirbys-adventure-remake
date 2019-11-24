using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_healthController : MonoBehaviour
{
    public int healthPoints;

    public void takeDamage()
    {
        healthPoints --;
        if(healthPoints <= 0)
        {
            Debug.Log("X_X");
        }
    }
}
