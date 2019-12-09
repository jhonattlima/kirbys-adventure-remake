using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBlue : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public Image[] bars;
    public Sprite body;
    public Sprite fullBar;
    public Sprite emptyBar;

    void Update()
    {

        //Não consegui encontrar uma forma simples de fazer surgir o 'body' pra deixar o código modular pra todas as cores.
        //'body' seria o painel de fundo que contém as barras

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        for (int i = 0; i < bars.Length; i++)
        {

            //Current health control
            if (i < currentHealth)
            {
                bars[i].sprite = fullBar;
            }
            else
            {
                bars[i].sprite = emptyBar;
            }

            // Max health control
            if (i < maxHealth)
            {
                bars[i].enabled = true;
            }
            else
            {
                bars[i].enabled = false;
            }
        }
    }
}
