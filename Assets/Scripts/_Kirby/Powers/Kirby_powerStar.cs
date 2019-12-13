using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerStar : MonoBehaviour
{
    public Powers power;
    private bool isActivated = false;

    private void Awake()
    {
        StartCoroutine(waitToBecomeActive());
    }

    private void Start() {
        Destroy(gameObject, KirbyConstants.KIRBY_STAR_COOLDOWN_TO_BE_ACTIVE + 5 );
        transform.LookAt(Camera.main.transform.position);
    }

    IEnumerator waitToBecomeActive()
    {
        yield return new WaitForSeconds(KirbyConstants.KIRBY_STAR_COOLDOWN_TO_BE_ACTIVE);
        isActivated = true;
    }

    public void setPushDirection(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce((direction + Vector3.up) * 400);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(KirbyConstants.TAG_PLAYER) && isActivated)
        {
            Debug.Log("Star: Kirby got power: " + power);
            other.gameObject.GetComponent<Kirby_healthController>().retrievePower(power);
            GameManager.instance.localPlayer.kirbyServerController.CmdDestroyPrefab(this.gameObject);
        }
    }
}
