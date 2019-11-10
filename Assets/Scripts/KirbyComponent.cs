using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyComponent : MonoBehaviour
{
    protected Kirby_actor Actor
    {
        get 
        {
            return _cachedActor ?? (_cachedActor = GetComponentInParent<Kirby_actor>());
        }
    }
    
    private Kirby_actor _cachedActor;

}
