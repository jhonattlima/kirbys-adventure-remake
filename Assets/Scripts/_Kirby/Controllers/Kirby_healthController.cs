using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_healthController : MonoBehaviour
{
    private Kirby_actor _kirby;
    private int healthPoints = KirbyConstants.PLAYER_HEALTH_POINTS;
    bool isTakingDamage = false;

    private void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    public void retrievePower(int power)
    {
        if (_kirby.enemy_powerInMouth == (int)Powers.None) return;
        switch (power)
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
        _kirby.enemy_powerInMouth = power;
        _kirby.hasPower = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!_kirby.isLocalPlayer) return;
        if (hit.gameObject.CompareTag(KirbyConstants.TAG_ENEMY)
            && !_kirby.isInvulnerable)
        {
            takeDamage(hit.gameObject.GetComponent<Enemy_actor>().touchDamage);
            hit.gameObject.GetComponent<Enemy_healthController>().takeDamage(KirbyConstants.PLAYER_NORMAL_DAMAGE);
        }
    }

    public void takeDamage(int amountOfDamage)
    {
        if (isTakingDamage) return;
        isTakingDamage = true;
        healthPoints -= amountOfDamage;
        Debug.Log("Ouch, took damage! Life points now: " + healthPoints);
        //UIPanelKirbyStatusController.instance.setLife(healthPoints);
        if (healthPoints <= 0)
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
        _kirby.kirbyServerController.CmdGameOverByDeath(_kirby.playerNumber);
    }

    IEnumerator sufferDamage()
    {
        _kirby.animator.SetTrigger(KirbyConstants.ANIM_NAME_TAKE_DAMAGE);
        _kirby.isParalyzed = true;
        _kirby.isInvulnerable = true;
        if (_kirby.hasPower)
        {
            expelStar();
            _kirby.powerBeam.enabled = false;
            _kirby.powerFire.enabled = false;
            _kirby.powerShock.enabled = false;
            _kirby.hasPower = false;
            _kirby.enemy_powerInMouth = (int)Powers.None;
        }

        // Vector3 movement;
        // if (_kirby.isLookingRight)
        // {
        //     movement = (_kirby.directionLeft * 3) * KirbyConstants.PUSH_SPEED_WHEN_DAMAGED * Time.deltaTime;
        // }
        // else
        // {
        //     movement = (_kirby.directionRight * 3) * KirbyConstants.PUSH_SPEED_WHEN_DAMAGED * Time.deltaTime;
        // }
        // _kirby.characterController.Move(movement);

        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_TO_RECOVER_FROM_DAMAGE);
        _kirby.isParalyzed = false;

        isTakingDamage = false;
        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_INVULNERABLE);
        _kirby.isInvulnerable = false;
    }

    // Method called by animation
    public void blink()
    {
        StartCoroutine(keepBlinking());
    }

    IEnumerator keepBlinking()
    {
        SkinnedMeshRenderer meshRenderer = _kirby.body.GetComponent<SkinnedMeshRenderer>();
        while (_kirby.isInvulnerable)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        meshRenderer.enabled = true;
    }

    private void expelStar()
    {
        Kirby_powerStar star = Instantiate(_kirby.starPrefab, transform.position + 0.1f * Vector3.up, transform.rotation).GetComponent<Kirby_powerStar>();
        star.power = _kirby.enemy_powerInMouth;
        star.setPushDirection(_kirby.isLookingRight ? _kirby.directionLeft : _kirby.directionRight);
    }
}
