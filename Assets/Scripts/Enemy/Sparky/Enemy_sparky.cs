﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sparky : MonoBehaviour
{
    private Enemy_actor _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy_actor>();

        _enemy.healthController.healthPoints = 1;
        _enemy.touchDamage = 1;
        _enemy.type = Powers.Shock;
    }
}
