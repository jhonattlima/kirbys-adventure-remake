using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_mouthController : MonoBehaviour
{
    private Kirby_actor _kirby;

    void Start()
    {
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(KirbyConstants.TAG_ENEMY) 
            && _kirby.isSucking)
        {
            _kirby.enemy_Actor = other.GetComponent<Enemy_actor>();
            _kirby.isFullOfEnemy = true;
            Destroy(other.gameObject);
        }
    }
}