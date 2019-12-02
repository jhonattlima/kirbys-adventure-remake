using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform exitSpot;
    public Transform newArea;

    private Kirby_actor kirby;

    private void OnTriggerStay(Collider other) 
    {
        kirby = other?.GetComponent<Kirby_actor>();
        if(!kirby || !kirby.isLocalPlayer) return;
        if(Input.GetKeyDown(KirbyConstants.KEY_ENTER_DOOR))
        {
            enterDoor();
        }
    }

    private void enterDoor()
    {
        kirby.characterController.Move(exitSpot.localPosition);
        kirby.currentArea = newArea;
        CameraController.instance.changeCameraRotation();
    }
}
