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

    public void TakeDamage(int damage)
    {
        Debug.Log("take damg");

        CmdTakeDamage(damage);
    }

    [Command]
    public void CmdTakeDamage(int damage)
    {
        RpcTakeDamage(damage);
        _enemy.healthController.takeDamage(damage);
    }

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        Debug.Log("take damg RpcTakeDamage");

        _enemy.healthController.takeDamage(damage);
    }
}
