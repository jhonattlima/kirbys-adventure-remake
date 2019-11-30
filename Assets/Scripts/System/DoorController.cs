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
        kirby.isParalyzed = true;
        //kirby.transform.Rotate(0, 90, 0);
        Camera.main.transform.Rotate(0, -90,0);
        kirby.characterController.Move(exitSpot.localPosition);
        kirby.currentArea = newArea;
        kirby.isParalyzed = false;
    }
}
