using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_SuckAreaController : MonoBehaviour
{
    private Transform transformMouth;
    private Kirby_actor _kirby;
    private float _suckSpeed = KirbyConstants.KIRBY_SUCK_SPEED;

    private void Start() {
        _kirby = GetComponentInParent<Kirby_actor>();
        transformMouth = _kirby.mouth.gameObject.transform;
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag(KirbyConstants.TAG_ENEMY) || other.GetComponent<Kirby_powerStar>())
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transformMouth.position, _suckSpeed * Time.deltaTime);
        }
    }
}
