using System;
using UnityEngine;

internal class RemoteController : MonoBehaviour
{

    public static RemoteController instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    internal void enterOnMatch(ButtonMatchController button)
    {
        //throw new NotImplementedException();
    }
}