using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerStar : MonoBehaviour
{
    public int power;
    private bool isActivated = false;

    private void Awake() 
    {
        StartCoroutine(waitToBecomeActive());
    }

    IEnumerator waitToBecomeActive()
    {
        yield return new WaitForSeconds(KirbyConstants.KIRBY_STAR_COOLDOWN_TO_BE_ACTIVE);
        GetComponent<Rigidbody>().isKinematic = false;
        isActivated = true;
    }

    public void setPushDirection(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce((direction + Vector3.up) * 5);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag(KirbyConstants.TAG_PLAYER) && isActivated)
        {
            Debug.Log("Star: Kirby got power: " + power);
            other?.gameObject?.GetComponent<Kirby_healthController>()?.retrievePower(power);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
