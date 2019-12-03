using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_serverController : NetworkBehaviour
{
    private Enemy_actor _enemy;

    public void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
    }

    [Command]
    public void CmdTakeDamage(int damage)
    {
        RpcTakeDamage(damage);
    }

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        _enemy.healthController.takeDamage(damage);
    }
}
