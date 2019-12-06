using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_featureInhaleExpel : NetworkBehaviour
{
    private Kirby_actor _kirby;

    private void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (!_kirby.isParalyzed)
        {
            checkSuckAction();
            checkActivatePowerAction();
        }
    }

    private void checkSuckAction()
    {
        if (Input.GetKey(KirbyConstants.KEY_SUCK))
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
                if (Input.GetKeyDown(KirbyConstants.KEY_SUCK))
                {
                    if (_kirby.isFullOfAir)
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

    private void checkActivatePowerAction()
    {
        if (Input.GetKeyDown(KirbyConstants.KEY_ACTIVATE_POWER))
        {
            if (_kirby.isFullOfEnemy
                && _kirby.enemy_powerInMouth != (int)Powers.None
                && !_kirby.hasPower)
            {
                activatePower();
            }
            _kirby.isFullOfEnemy = false;
        }
    }

    public void activatePower()
    {
        if (_kirby.enemy_powerInMouth == (int)Powers.None) return;
        Debug.Log("Activated power");
        switch (_kirby.enemy_powerInMouth)
        {
            case (int)Powers.Fire:
                _kirby.powerFire.enabled = true;
                break;
            case (int)Powers.Shock:
                _kirby.powerShock.enabled = true;
                break;
            case (int)Powers.Beam:
                _kirby.powerBeam.enabled = true;
                break;
        }
        //UIPanelLifePowercontroller.instance.setPower(_kirby.enemy_powerInMouth);
        _kirby.hasPower = true;
    }

    public void suckOn()
    {
        //_kirby.mouth.gameObject.SetActive(true);
        //_kirby.areaOfSucking.gameObject.SetActive(true);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, true);
        _kirby.isSucking = true;
    }

    public void suckOff()
    {
        //_kirby.mouth.gameObject.SetActive(false);
        //_kirby.areaOfSucking.gameObject.SetActive(false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
        _kirby.isSucking = false;
    }

    public void expelAir()
    {
        Kirby_powerAirBall airBall = Instantiate(_kirby.airPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerAirBall>();
        airBall.GetComponent<Kirby_powerAirBall>()._kirby = _kirby;
        airBall.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfAir = false;
    }

    private void expelEnemy()
    {
        Kirby_powerStarBullet starBullet = Instantiate(_kirby.starBulletPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerStarBullet>();
        starBullet.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfEnemy = false;
        _kirby.enemy_powerInMouth = (int)Powers.None;
    }
}
