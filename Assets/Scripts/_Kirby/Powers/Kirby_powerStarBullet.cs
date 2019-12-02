using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerStarBullet : MonoBehaviour
{
    public bool isLocalClient;
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

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (!isLocalClient) return;
        other?.GetComponent<Enemy_serverController>()?.CmdTakeDamage(damage);
        Destroy(gameObject);
    }
}
