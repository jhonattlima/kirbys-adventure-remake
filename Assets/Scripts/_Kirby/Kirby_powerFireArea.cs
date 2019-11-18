using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerFireArea : MonoBehaviour
{
   private void OnTriggerStay(Collider other) {
       other?.GetComponent<Enemy_healthController>()?.takeDamage();
   }
}
