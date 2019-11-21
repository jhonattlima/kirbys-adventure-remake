﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_healthController : MonoBehaviour
{
    private Kirby_actor _kirby;
    private int healthPoints = KirbyConstants.PLAYER_HEALTH_POINTS;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    public void retrievePower(int power)
    {
        if(_kirby.enemy_powerInMouth == (int)Powers.None) return;
        switch (power)
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
        _kirby.enemy_powerInMouth = power;
        _kirby.hasPower = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.CompareTag(KirbyConstants.TAG_ENEMY)
            && !_kirby.isInvulnerable)
        {
            takeDamage(hit.gameObject.GetComponent<Enemy_actor>().touchDamage);
        }
    }

    public void takeDamage(int amountOfDamage)
    {
        healthPoints -= amountOfDamage;
        if(healthPoints <= 0)
        {
            die();
        } 
        else 
        {
            StartCoroutine(sufferDamage());
        }
    }

    private void die()
    {
        // runs death animation;
        // Destroys object;
        // Return to main lobby;
    }

    IEnumerator sufferDamage()
    {
        Debug.Log("Ouch! took Damage Kirby's life: " + healthPoints);
        _kirby.isParalyzed = true;

        // Play damaged animation

        if(_kirby.hasPower)
        {
            expelStar();
        }

        Vector3 movement;
        if(_kirby.isLookingRight)
        {
            movement = _kirby.directionLeft * KirbyConstants.PUSH_SPEED_WHEN_DAMAGED * Time.deltaTime;
        }
        else 
        {
            movement = _kirby.directionRight * KirbyConstants.PUSH_SPEED_WHEN_DAMAGED * Time.deltaTime;
        }
        _kirby.characterController.Move(movement);
        
        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_TO_RECOVER_FROM_DAMAGE);
        _kirby.isParalyzed = false;

        // Stop playing damaged animation
        // Play invulnerable animation

        StartCoroutine(stayInvulnerable());
    }

    // poops a star
    // and player has no power anymore
    private void expelStar()
    {
        Kirby_powerStar star  = Instantiate(_kirby.starPrefab, transform.position, transform.rotation).GetComponent<Kirby_powerStar>();
        star.power = _kirby.enemy_powerInMouth;
        star.setPushDirection(_kirby.isLookingRight ? _kirby.directionLeft : _kirby.directionRight);
        _kirby.hasPower = false;
        _kirby.enemy_powerInMouth = (int)Powers.None;
    }

    IEnumerator stayInvulnerable()
    {
        _kirby.isInvulnerable = true;
        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_INVULNERABLE);
        _kirby.isInvulnerable = false;
    }
}
