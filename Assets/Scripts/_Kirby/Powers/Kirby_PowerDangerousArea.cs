using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_PowerDangerousArea : MonoBehaviour
{
    public int damage = KirbyConstants.PLAYER_NORMAL_DAMAGE;

    private Kirby_actor _kirby;

    private void Start() {
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    private void OnTriggerStay(Collider other) 
    {
        if(!_kirby.isLocalPlayer) return;
        other?.GetComponent<Enemy_serverController>()?.CmdTakeDamage(damage);
    }
}
