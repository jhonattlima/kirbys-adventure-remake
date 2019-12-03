using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_PowerDangerousArea : MonoBehaviour
{
    public int damage = KirbyConstants.PLAYER_NORMAL_DAMAGE;

    public Kirby_actor _kirby;

    private void Start()
    {
        if(!_kirby) _kirby = GetComponentInParent<Kirby_actor>();
    }

    private void OnTriggerStay(Collider other) 
    {
        if(!_kirby.isLocalPlayer) return;
        other?.GetComponent<Enemy_serverController>()?.CmdTakeDamage(damage);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!_kirby.isLocalPlayer) return;
        //other?.GetComponent<Enemy_serverController>()?.CmdTakeDamage(damage);
        if(other.GetComponent<Enemy_serverController>()){
            other.GetComponent<Enemy_serverController>().CmdTakeDamage(damage);
            Debug.Log("Hit an enemy");
        }
    }
}
