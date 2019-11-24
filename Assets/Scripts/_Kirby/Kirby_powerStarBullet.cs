using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerStarBullet : MonoBehaviour
{
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
        other?.GetComponent<Enemy_healthController>()?.takeDamage();
        Destroy(gameObject);
    }
}
