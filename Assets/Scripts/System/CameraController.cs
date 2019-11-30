using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Kirby_actor localKirby;
    public CameraController instance;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 kirbyPosition = localKirby.transform.position;
        Vector3 cameraPosition = kirbyPosition; // + localKirby.directionBack * 6;
        cameraPosition.y = transform.position.y;

        transform.position = cameraPosition;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(localKirby.directionForward), 1.0f * Time.deltaTime);
    }
}
