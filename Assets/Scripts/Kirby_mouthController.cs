using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_mouthController : MonoBehaviour
{
    private Kirby_actor _kirby;

    // Start is called before the first frame update
    void Start()
    {
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(KirbyConstants.tagEnemy) 
            && _kirby.suckArea.gameObject.activeSelf)
        {
            // give powers to Kirby
            if(!_kirby.hasPower 
                && other.GetComponent<Enemy_actor>() 
                && other.GetComponent<Enemy_actor>().hasPower)
            {

            }
            Destroy(other.gameObject);
        }
    }

}
