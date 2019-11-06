using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_mouthcontroller : MonoBehaviour
{
    Kirby_actor kirby;

    // Start is called before the first frame update
    void Start()
    {
        kirby = GetComponentInParent<Kirby_actor>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(KirbyConstants.tagEnemy) 
            && kirby.suckArea.gameObject.activeSelf)
        {
            // give powers to Kirby
            if(!kirby.hasPower)
            {

            }
            Destroy(other.gameObject);
        }
    }

}
