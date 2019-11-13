using System;
using System.Collections;
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
        checkSuckAction();
        checkActivatePowerAction();
    }

    private void checkSuckAction()
    {
        if (!_kirby.isFullOfAir 
            && !_kirby.isFullOfEnemy 
            && _kirby.characterController.isGrounded
            && Input.GetKey(KirbyConstants.KEY_SUCK))
        {
            suckOn();
            _kirby.isSucking = true;
        }
        else if(Input.GetKeyDown(KirbyConstants.KEY_SUCK))
        {
            if(_kirby.isFullOfAir)
            {
                expelAir();
            } 
        }
        else 
        {
            suckOff();
        }
    }

    private void checkActivatePowerAction()
    {
        if (Input.GetKeyDown(KirbyConstants.KEY_ACTIVATE_POWER))
        {
            if(_kirby.isFullOfEnemy
                && _kirby.enemy_Actor.hasPower)
            {
                activatePower();
            } 
            else if (_kirby.isFullOfEnemy 
                && !_kirby.enemy_Actor.hasPower)
            {
                _kirby.enemy_Actor = null;
                _kirby.isFullOfEnemy = false;
            }
        }
    }

    public void suckOn()
    {
        _kirby.suckArea.gameObject.SetActive(true);
        _kirby.isSucking = true;
    }

    public void suckOff()
    {
        _kirby.suckArea.gameObject.SetActive(false);
        _kirby.isSucking = false;
    }

        // TO DO
    // if fullOfair
    // then throw a low range cloud
    // and is not fullOfair anymore
    public void expelAir()
    {
        GameObject airBall = Instantiate(_kirby.airPrefab, _kirby.transform.position, _kirby.transform.rotation);
        airBall.GetComponent<Kirby_powerAirBall>()
            .setBulletDirection(GetComponent<Kirby_movement>()._isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfAir = false;
    }

    // TO DO
    // and kirby is fullOfenemy
    // if enemy has no power
    // and player presses poop key
    // poops a star
    private void expelStar()
    {
        
    }

    // TO DO
    // If kirby is full of enemy
    // and enemy has power
    // and player presses getPowerKey
    // Then activates power
    public void activatePower()
    {
        switch (_kirby.enemy_Actor.type)
        {
            case (int) Powers.Fire:
                break;
            case (int) Powers.Shock:
                break;
            case (int) Powers.Beam:
                break;
        }
    }

    // TO DO
    // if kirby is fullOfenemy
    // and player presses keySuck
    // Then throws a horizontal move star
    // And is not fullOfenemy anymore
    private void expelEnemy()
    {
        
    }
}
