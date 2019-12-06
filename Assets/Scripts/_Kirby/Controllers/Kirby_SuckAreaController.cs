using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_SuckAreaController : MonoBehaviour
{
    private Transform transformMouth;
    private Kirby_actor _kirby;
    float suckSpeed = KirbyConstants.KIRBY_SUCK_SPEED;

    private void Start()
    {
        _kirby = GetComponentInParent<Kirby_actor>();
        transformMouth = _kirby.mouth.gameObject.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_kirby.isLocalPlayer) return;
        if (other.CompareTag(KirbyConstants.TAG_ENEMY) || other.GetComponent<Kirby_powerStar>())
        {
            if (_kirby.isLookingRight) other.GetComponent<CharacterController>().Move(transformMouth.position.normalized * Time.deltaTime * suckSpeed * -1);
            else other.GetComponent<CharacterController>().Move(transformMouth.position.normalized * Time.deltaTime * suckSpeed);
        }
    }
}
