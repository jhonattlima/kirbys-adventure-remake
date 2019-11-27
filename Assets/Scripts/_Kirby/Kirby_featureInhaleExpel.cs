using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_featureInhaleExpel : NetworkBehaviour 
{ 
    private Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if(!isLocalPlayer) return;
        if(!_kirby.isParalyzed)
        {
            checkSuckAction();
            checkActivatePowerAction();
        }
    }

    private void checkSuckAction()
    {   
        if(Input.GetKey(KirbyConstants.KEY_SUCK))
        {
            if (!_kirby.isFullOfAir
                && !_kirby.isFullOfEnemy 
                && _kirby.enemy_powerInMouth == (int)Powers.None
                && _kirby.characterController.isGrounded)
            {
                suckOn();
            }
            else
            {
                if(Input.GetKeyDown(KirbyConstants.KEY_SUCK))
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
        }
        else 
        {
            suckOff();
        }
    }

    // If activate power key is pressed
    // If kirby has an enemy in mouth and it has power
    // then activate de power
    // anyway, swallow enemy
    private void checkActivatePowerAction()
    {
        if (Input.GetKeyDown(KirbyConstants.KEY_ACTIVATE_POWER))
        {
            if(_kirby.isFullOfEnemy 
                && _kirby.enemy_powerInMouth != (int)Powers.None
                && !_kirby.hasPower)
            {
                activatePower();
            } 
            _kirby.isFullOfEnemy = false;
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

    // Throw a low range Air Ball
    // and is not fullOfair anymore
    public void expelAir()
    {
        Kirby_powerAirBall airBall = Instantiate(_kirby.airPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerAirBall>();
        airBall.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfAir = false;
    }

    // Throws a horizontal move star
    // And is not fullOfenemy anymore
    private void expelEnemy()
    {
        Kirby_powerStarBullet starBullet = Instantiate(_kirby.starBulletPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerStarBullet>();
        starBullet.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfEnemy = false;
        _kirby.enemy_powerInMouth = (int)Powers.None;
    }

    // If enemy in mouth has power
    // then ativate power script
    // and has power now
    public void activatePower()
    {
        if(_kirby.enemy_powerInMouth == (int)Powers.None) return;
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
    }
}
