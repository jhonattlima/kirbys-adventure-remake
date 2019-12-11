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
        if (other.CompareTag(KirbyConstants.TAG_ENEMY))
        {
            Vector3 moveTarget = transformMouth.position;
            Vector3 currentPosition = other.transform.position;
            Vector3 moveDirection = (moveTarget - currentPosition).normalized;

            other.GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime * suckSpeed);
        }
    }
}
