using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        if (died) return;
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            died = true;
            Debug.Log("Enemy hit.");

            if (_enemy.isServer)
            {
                if (!_enemy.animator.GetBool(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE)) _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, true);
            }
            else
            {
                if (!_enemy.animator.GetBool(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE))
                {
                    _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, true);
                }
                GameManager.instance.localPlayer.GetComponent<Kirby_serverController>().changeBoolAnimationStatus(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE, true, this.gameObject);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(KirbyConstants.TAG_PLAYER))
        {
            if (died || !hit.gameObject.GetComponent<Kirby_actor>().isLocalPlayer) return;

            Debug.Log("EnemyHealthController: Enemy hit someone.");
            takeDamage(KirbyConstants.PLAYER_NORMAL_DAMAGE);
            hit.gameObject.GetComponent<Kirby_healthController>().takeDamage(_enemy.touchDamage);
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
