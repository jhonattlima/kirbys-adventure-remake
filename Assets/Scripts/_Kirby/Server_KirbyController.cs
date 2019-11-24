using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server_KirbyController : NetworkBehaviour
{
    private Kirby_actor _kirby;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
    }

    [Command]
    public void CmdFireOn()
    {
        _kirby.powerFire.fireOn();
        RpcFireOn();
    }

    [ClientRpc]
    public void RpcFireOn()
    {
        _kirby.powerFire.fireOn();
    }

    [Command]
    public void CmdFireOff()
    {
        _kirby.powerFire.fireOff();
        RpcFireOff();
    }

    [ClientRpc]
    public void RpcFireOff()
    {
        _kirby.powerFire.fireOff();
    }

    [Command]
    public void CmdShockOn()
    {
        _kirby.powerShock.shockOn();
        RpcShockOn();
    }

    [ClientRpc]
    public void RpcShockOn()
    {
        _kirby.powerShock.shockOn();
    }

    [Command]
    public void CmdShockOff()
    {
        _kirby.powerShock.shockOff();
        RpcShockOff();
    }

    [ClientRpc]
    public void RpcShockOff()
    {
        _kirby.powerShock.shockOff();
    }

    [Command]
    public void CmdActivateBeam()
    {
        _kirby.powerBeam.activateBeam();
        RpcActivateBeam();
    }

    [ClientRpc]
    public void RpcActivateBeam()
    {
        _kirby.powerBeam.activateBeam();
    }
}
