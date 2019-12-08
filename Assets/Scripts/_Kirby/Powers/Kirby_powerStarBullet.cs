using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerStarBullet : MonoBehaviour
{
    public Kirby_actor _kirby;
    public int damage = KirbyConstants.PLAYER_NORMAL_DAMAGE;

    private float _starSpeed = KirbyConstants.KIRBY_STAR_BULLET_SPEED;
    private Vector3 direction;

    void Update()
    {
        transform.position += direction * _starSpeed * Time.deltaTime;
    }

    public void setBulletDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KirbyConstants.TAG_PLAYER)) return;
        if (!_kirby.isLocalPlayer) return;
        if (other.GetComponent<Enemy_actor>())
        {
            GameManager.instance.localPlayer.kirbyServerController.CmdDealDamageToMob(other.gameObject, KirbyConstants.PLAYER_NORMAL_DAMAGE);
            Destroy(gameObject);
        }
    }
}
