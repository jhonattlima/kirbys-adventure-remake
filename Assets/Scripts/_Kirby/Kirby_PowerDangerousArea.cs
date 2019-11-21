using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_PowerDangerousArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        other?.GetComponent<Enemy_healthController>()?.takeDamage();
    }
}
