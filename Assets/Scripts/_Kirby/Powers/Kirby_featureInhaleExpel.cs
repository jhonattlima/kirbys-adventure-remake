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
            else
            {
                AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbySwallow);
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
        AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyAbsorb);
    }

    public void suckOn()
    {
        if(!_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK))
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, true);
            _kirby.isSucking = true;
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyInhale);
        }
    }

    public void suckOff()
    {
        if(_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK))
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
            _kirby.isSucking = false;
            AudioPlayerSFXController.instance._audioSource.Stop();
        }
    }

    public void expelAir()
    {
        if(!_kirby.animator.GetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL))
        {
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, true, this.gameObject);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, true);
            _kirby.isFullOfAir = false;
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyExpelAirBall);
        }
    }

    // Method called by Animator
    public void finishExpelAir()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, false);
    }

    private void expelEnemy()
    {
        Kirby_powerStarBullet starBullet = Instantiate(_kirby.starBulletPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerStarBullet>();
        starBullet._kirby = _kirby;
        starBullet.setBulletDirection(_kirby.isLookingRight ? _kirby.directionRight : _kirby.directionLeft);
        _kirby.isFullOfEnemy = false;
        _kirby.enemy_powerInMouth = (int)Powers.None;
        AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyExpelStar);
        if(!isServer) _kirby.kirbyServerController.CmdSpawnStarBulletPrefab(this.gameObject, _kirby.isLookingRight);
        else _kirby.kirbyServerController.RpcSpawnStarBulletPrefab(this.gameObject, _kirby.isLookingRight);
    }
}
