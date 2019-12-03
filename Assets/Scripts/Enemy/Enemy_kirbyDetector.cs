using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_kirbyDetector : MonoBehaviour
{
    Enemy_actor _enemy;

    void Start()
    {
        _enemy = GetComponentInParent<Enemy_actor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER)) _enemy.isKirbyClose = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER)) _enemy.isKirbyClose = false;
    }
    
}
