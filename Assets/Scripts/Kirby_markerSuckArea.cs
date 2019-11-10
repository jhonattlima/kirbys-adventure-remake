using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_markerSuckArea : MonoBehaviour
{
    private Transform transformMouth;
    private Kirby_actor kirby;
    private float _suckSpeed = 1f;

    private void Start() {
        kirby = GetComponentInParent<Kirby_actor>();
        transformMouth = kirby.mouth.gameObject.transform;
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag(KirbyConstants.tagEnemy))
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transformMouth.position, _suckSpeed * Time.deltaTime);
        }
    }
}
