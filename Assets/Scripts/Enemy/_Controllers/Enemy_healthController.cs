using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_healthController : MonoBehaviour
{
    public int healthPoints;
    public Enemy_actor _enemy;
    public bool died = false;

    private void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
    }

    public void takeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0 && !died)
        {
            died = true;
            GameManager.instance.localPlayer.GetComponent<Kirby_serverController>().changeBoolAnimationStatus(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, true, this.gameObject);
            //_enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Kirby_actor>())
        {
            if (!hit.gameObject.GetComponent<Kirby_actor>().isLocalPlayer) return;
            if (died) return;
            died = true;
            Debug.Log("EnemyHealthController: Enemy hit someone.");
            hit.gameObject.GetComponent<Kirby_healthController>().takeDamage(_enemy.touchDamage);
            GameManager.instance.localPlayer.GetComponent<Kirby_serverController>().changeBoolAnimationStatus(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, true, this.gameObject);
            // if(_enemy.isServer) _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE);
            // else GameManager.instance.localPlayer.GetComponent<Kirby_serverController>().CmdChangeTriggerAnimation(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, this.gameObject);
        }
    }

    //Method called by attack animation
    public void finishAttack()
    {
        _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_ATTACK, false);
    }

    // Method called by death animation
    public void die()
    {
        Destroy(gameObject);
    }
}
