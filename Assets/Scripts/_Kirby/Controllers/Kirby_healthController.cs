using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Kirby_healthController : NetworkBehaviour
{
    Kirby_actor _kirby;
    int healthPoints = KirbyConstants.PLAYER_HEALTH_POINTS;
    bool isTakingDamage = false;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!_kirby.isLocalPlayer) return;
        if (hit.gameObject.CompareTag(KirbyConstants.TAG_ENEMY)
            && !_kirby.isInvulnerable)
        {
            Debug.Log("KirbyHealthController: Kirby hit someone.");
            takeDamage(hit.gameObject.GetComponent<Enemy_actor>().touchDamage);
            hit.gameObject.GetComponent<Enemy_healthController>().takeDamage(KirbyConstants.PLAYER_NORMAL_DAMAGE);
        }
    }

    public void takeDamage(int amountOfDamage)
    {
        if (isTakingDamage || !_kirby.isLocalPlayer) return;
        isTakingDamage = true;
        healthPoints -= amountOfDamage;
        Debug.Log("Ouch, took damage! Life points now: " + healthPoints);
        //UIPanelKirbyStatusController.instance.setLife(healthPoints);
        if (healthPoints <= 0) _kirby.kirbyServerController.CmdGameOverByDeath(_kirby.playerNumber); //died
        else StartCoroutine(sufferDamage());
    }

    IEnumerator sufferDamage()
    {
        if(!_kirby.animator.GetBool(KirbyConstants.ANIM_NAME_TAKE_DAMAGE))
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_NAME_TAKE_DAMAGE, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_NAME_TAKE_DAMAGE, true, this.gameObject);
        }

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
        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_TO_RECOVER_FROM_DAMAGE);
        _kirby.isParalyzed = false;

        isTakingDamage = false;
        yield return new WaitForSeconds(KirbyConstants.COOLDOWN_INVULNERABLE);
        _kirby.isInvulnerable = false;
    }

    public void retrievePower(int power)
    {
        if (_kirby.enemy_powerInMouth != (int)Powers.None) return;
        switch ((Powers)power)
        {
            case Powers.Fire:
                _kirby.powerFire.enabled = true;
                break;
            case Powers.Shock:
                _kirby.powerShock.enabled = true;
                break;
            case Powers.Beam:
                _kirby.powerBeam.enabled = true;
                break;
        }
        
        Debug.Log("Entered on retrieve power");
        _kirby.enemy_powerInMouth = power;
        _kirby.isFullOfAir = false;
        _kirby.isFullOfEnemy = false;
        _kirby.hasPower = true;
    }

    private void expelStar()
    {
        if (_kirby.isServer)
        {
            Kirby_powerStar star = Instantiate(PrefabsAndInstancesLibrary.instance.starPower, _kirby.spotToDropStar.position, _kirby.spotToDropStar.rotation).GetComponent<Kirby_powerStar>();
            NetworkServer.Spawn(star.gameObject);
            star.power = _kirby.enemy_powerInMouth;
            star.setPushDirection(-_kirby.spotToDropStar.transform.forward);
        }
        else _kirby.kirbyServerController.CmdSpawnStarPowerPrefab(this.gameObject);
    }

    // Method called by animation
    public void blink()
    {
        StartCoroutine(keepBlinking());
    }

    // Method called by animation
    public void finishGetDamagedAnimation()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_NAME_TAKE_DAMAGE, false);
    }

    IEnumerator keepBlinking()
    {
        while (_kirby.isInvulnerable)
        {
            foreach (SkinnedMeshRenderer mesh in _kirby.body)
            {
                mesh.enabled = !mesh.enabled;
            }
            yield return new WaitForSeconds(0.1f);
        }
        foreach (SkinnedMeshRenderer mesh in _kirby.body)
        {
            mesh.enabled = true;
        }
    }
}
