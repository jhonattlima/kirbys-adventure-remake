﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_featureInhaleExpel : MonoBehaviour
{
    private Kirby_actor _kirby;    

    void Start()
    {
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    void Update()
    {
        suck();
    }

    // If kirby is in the ground
    // And is not full of enemy or air
    // then startSucking
    // Set the bool to suck for true
    // else set the bool to suck for false
    private void suck()
    {
        if(!_kirby.isFullOfAir
            && !_kirby.isFullOfEnemy
            && _kirby.characterController.isGrounded
            && Input.GetKey(KirbyConstants.KEY_SUCK))
        {
            _kirby.suckArea.gameObject.SetActive(true);
            _kirby.isSucking = true;
        } 
        else
        {
            _kirby.suckArea.gameObject.SetActive(false);
            _kirby.isSucking = false;
        }
    }

    // TO DO
    // If kirby is full of enemy
    // and enemy has power
    // and player presses getPowerKey
    // Then activates power
    private void ActivatePower()
    {
        if(_kirby.isFullOfEnemy
            && _kirby.enemy_Actor.hasPower
            && Input.GetKeyDown(KirbyConstants.KEY_ACTIVATE_POWER))
        {
            switch (_kirby.enemy_Actor.type)
            {
                //TODO
            }
        }
    }

    // TO DO
    // if kirby  is in the air
    // and if fullOfair
    // then throw a low range cloud
    // and is not fullOfair anymore
    public void expelAir()
    {
        
    }

    // TO DO
    // if kirby has power
    // and player presses poop key
    // poops a star
    public void expelStar()
    {
        
    }

    // TO DO
    // if kirby is fullOfenemy
    // and player presses keySuck
    // Then throws a horizontal move star
    // And is not fullOfenemy anymore
    public void expelEnemy()
    {
        
    }
}
