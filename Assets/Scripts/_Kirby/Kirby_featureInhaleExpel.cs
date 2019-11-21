using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_featureInhaleExpel : MonoBehaviour
{ 
    private Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(!_kirby.isParalyzed)
        {
            checkSuckAction();
            checkActivatePowerAction();
        }
    }

    private void checkSuckAction()
    {   if(Input.GetKey(KirbyConstants.KEY_SUCK))
        {
             if (!_kirby.isFullOfAir
                && !_kirby.hasPower 
                && !_kirby.isFullOfEnemy 
                && _kirby.characterController.isGrounded)
                
            {
                suckOn();
            }
            else
            {
                if(_kirby.isFullOfAir)
                {
                    expelAir();
                } 
                else if (_kirby.isFullOfEnemy)
                {
                    expelEnemy();
                }
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
                && _kirby.enemy_powerInMouth != (int)Powers.None)
            {
                activatePower();
            } 
            else if (_kirby.isFullOfEnemy 
                && _kirby.enemy_powerInMouth == (int)Powers.None)
            {
                _kirby.isFullOfEnemy = false;
            }
        }
    }

    public void suckOn()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, true);
    }

    public void suckOff()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
    }

    // If fullOfair
    // then throw a low range cloud
    // and is not fullOfair anymore
    public void expelAir()
    {
        Kirby_powerAirBall airBall = Instantiate(_kirby.airPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerAirBall>();
        airBall.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfAir = false;
    }

    // If full of enemy
    // and enemy has power
    // and player presses getPowerKey
    // Then activates power
    public void activatePower()
    {
        switch (_kirby.enemy_powerInMouth)
        {
            case (int) Powers.Fire:
                _kirby.powerFire.enabled = true;
                break;
            case (int) Powers.Shock:
                _kirby.powerShock.enabled = true;
                break;
            case (int) Powers.Beam:
                _kirby.powerBeam.enabled = true;
                break;
        }
        _kirby.hasPower = true;
        _kirby.isFullOfEnemy = false;
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
