using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_healthController : MonoBehaviour
{
    public int healthPoints;
    public Enemy_actor _enemy;
    public bool died = false;

    private void Start() {
        _enemy = GetComponent<Enemy_actor>();
    }

    public void takeDamage(int damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0 && !died)
        {
            died = true;
            StartCoroutine(triggerDeathAnimation());
        }
    }

    IEnumerator triggerDeathAnimation()
    {
        _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_TAKE_DAMAGE);
        yield return new WaitForSeconds(_enemy.animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
