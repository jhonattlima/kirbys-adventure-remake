using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerAirBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!GetComponentInParent<Kirby_actor>().isLocalPlayer) return;
        if (other.CompareTag(KirbyConstants.TAG_ENEMY))
        {
            GetComponentInParent<Kirby_actor>().animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, false);
            GameManager.instance.localPlayer.kirbyServerController.CmdDealDamageToMob(other.gameObject, KirbyConstants.PLAYER_NORMAL_DAMAGE);
            this.gameObject.SetActive(false);
        }
    }
}
