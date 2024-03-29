﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_SuckAreaController : MonoBehaviour 
{
    private Transform transformMouth;
    private Kirby_actor _kirby;
    private float _suckSlowliness = KirbyConstants.KIRBY_SUCK_SLOWLINESS;

    private void Start() {
        _kirby = GetComponentInParent<Kirby_actor>();
        transformMouth = _kirby.mouth.gameObject.transform;
    }

    private void OnTriggerStay(Collider other) {
        if(!_kirby.isLocalPlayer) return;
        if(other.CompareTag(KirbyConstants.TAG_ENEMY) || other.GetComponent<Kirby_powerStar>())
        {
            other.GetComponent<CharacterController>().Move(transformMouth.position * Time.deltaTime / _suckSlowliness);
        }
    }
}
