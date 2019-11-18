using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerBeamArea : MonoBehaviour
{
    private Kirby_actor _kirby;
    private Transform _originalPosition;
    private Quaternion _endRotation;
    private float _turnSpeed = 1f;

    private void Awake() {
        _originalPosition = transform;
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    private void Update() {
        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
        // transform.RotateAround()
    }

    private void OnTriggerStay(Collider other) 
    {
        other?.GetComponent<Enemy_healthController>()?.takeDamage();
    }

    private void OnEnable() 
    {
        StartCoroutine(beamShot());
    }

    IEnumerator beamShot()
    {
        // transform.position = _originalPosition.position;
        // _endRotation = GetComponentInParent<Kirby_movement>()._endRotation;
        // while(transform.rotation != _endRotation) yield return null;
        yield return new WaitUntil(() => transform.rotation != _endRotation);
        GetComponentInParent<Kirby_powerBeam>().beamOff();
    }
}
