using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleOfDeathController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            other.GetComponent<Kirby_healthController>().takeDamage(10);
        }
    }
}

