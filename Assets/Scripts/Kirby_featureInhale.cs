using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_featureInhale : MonoBehaviour
{
    private Kirby_actor _kirby;    

    void Awake()
    {
        _kirby = GetComponentInParent<Kirby_actor>();
    }

    // Update is called once per frame
    void Update()
    {
        suck();
    }

    private void suck()
    {
        if(Input.GetKey(KirbyConstants.keySuck))
        {
            _kirby.suckArea.gameObject.SetActive(true);
        } 
        else
        {
            _kirby.suckArea.gameObject.SetActive(false);
        }
    }
}
