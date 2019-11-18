using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_healthController : MonoBehaviour
{
    private Kirby_actor _kirby;
    private int healthPoints = KirbyConstants.PLAYER_HEALTH_POINTS;
    private bool isInvulnerable;
    private float invulnerableCoolDown = 0;

    private void Start() {
        _kirby = GetComponent<Kirby_actor>();
    }

    private void Update() 
    {

        if(invulnerableCoolDown > 0)
        {
            invulnerableCoolDown -= Time.deltaTime;
        }

        if(invulnerableCoolDown <= 0 && isInvulnerable)
        {
            isInvulnerable = false;
            // finish invulnerable animation
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
            // Start invulnerable animation
            StartCoroutine(sufferDamage());
        }
    }

    // poops a star
    // and player has no power anymore
    private void expelStar()
    {
        
        _kirby.hasPower = false;
        _kirby.enemy_powerInMouth = (int)Powers.None;
    }

    private void die()
    {
        // runs death animation;
        // Destroys object;
        // Return to main lobby;
    }

    IEnumerator sufferDamage()
    {
        Debug.Log("Ouch! took Damage");
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
        invulnerableCoolDown = KirbyConstants.COOLDOWN_INVULNERABLE;
        // Play invulnerable animation
    }
}
