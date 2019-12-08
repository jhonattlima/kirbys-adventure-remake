using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerAirBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<Kirby_actor>().isLocalPlayer) return;
        if (other.CompareTag(KirbyConstants.TAG_ENEMY))
        {
            GameManager.instance.localPlayer.kirbyServerController.CmdDealDamageToMob(other.gameObject, KirbyConstants.PLAYER_NORMAL_DAMAGE);
            this.gameObject.SetActive(false);
        }
    }
}
