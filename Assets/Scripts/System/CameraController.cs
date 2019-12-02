using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Kirby_actor localKirby;
    public static CameraController instance;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!localKirby) return;
        Vector3 kirbyPosition = localKirby.transform.position;
        Vector3 cameraPosition = kirbyPosition; // + localKirby.directionBack * 6;
        cameraPosition.y = transform.position.y;

        transform.position = cameraPosition;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(localKirby.directionForward), 1.0f * Time.deltaTime);
    }

    public void changeCameraRotation()
    {
        StartCoroutine(waitWhileChangeCameraRotation());
    }

    IEnumerator waitWhileChangeCameraRotation()
    {
        Debug.Log("Kirb is waiting");
        localKirby.isParalyzed = true;
        //Debug.Log("Kirb is waiting" + localKirby.isParalyzed);
        yield return new WaitForSeconds(10);
        //while (transform.rotation != Quaternion.LookRotation(localKirby.directionForward)) yield return null;
        //yield return new WaitUntil(()=> transform.rotation != Quaternion.LookRotation(localKirby.directionForward));
        Debug.Log("Kirb is waiting2" + localKirby.isParalyzed);
        localKirby.isParalyzed = false;
        Debug.Log("Kirb is waiting" + localKirby.isParalyzed);

    }
}
