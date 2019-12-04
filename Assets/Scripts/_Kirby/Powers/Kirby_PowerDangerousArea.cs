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

    void OnTriggerEnter(Collider other)
    {
        if(!_kirby.isLocalPlayer) return;
        if(other.GetComponent<Enemy_actor>())
        {
            GameManager.instance.localPlayer.kirbyServerController.CmdDealDamageToMob( other.gameObject ,KirbyConstants.PLAYER_NORMAL_DAMAGE);
        }
    }
}
