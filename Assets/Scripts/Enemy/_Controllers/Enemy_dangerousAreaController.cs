using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dangerousAreaController : MonoBehaviour
{
    int powerDamage = 1;

    void OnTriggerEnter(Collider other)
    {
        execKirbyDamage(other);
    }

    void OnTriggerStay(Collider other)
    {
        execKirbyDamage(other);
    }

    void execKirbyDamage(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            if(!other.GetComponent<Kirby_actor>().isLocalPlayer) return;
            other.gameObject.GetComponent<Kirby_healthController>().takeDamage(powerDamage);
        }
    }
}
