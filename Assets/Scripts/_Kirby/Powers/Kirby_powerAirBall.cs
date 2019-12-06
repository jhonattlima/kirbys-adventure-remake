using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerAirBall : MonoBehaviour
{
    private float _airSpeed = 5f;
    private Vector3 direction;
    public Kirby_actor _kirby;

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {
        transform.position += direction * _airSpeed * Time.deltaTime;
    }

    public void setBulletDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_kirby.isLocalPlayer) return;
        if (other.GetComponent<Enemy_actor>())
        {
            GameManager.instance.localPlayer.kirbyServerController.CmdDealDamageToMob(other.gameObject, KirbyConstants.PLAYER_NORMAL_DAMAGE);
            Destroy(gameObject);
        }
    }
}
