using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthPink : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public Image[] bars;
    public Sprite fullBar;
    public Sprite emptyBar;

    void Update()
    {
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        for (int i = 0; i < bars.Length; i++) {

            //Current health control
            if (i < currentHealth)
            {
                bars[i].sprite = fullBar;
            } else {
                bars[i].sprite = emptyBar;
            }

            // Max health control
            if (i < maxHealth) {
                bars[i].enabled = true;
            } else {
                bars[i].enabled = false;
            }
        }
    }
}
