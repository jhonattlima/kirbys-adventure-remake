using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkController : NetworkManager
{
    // Variables
    public static event Action<NetworkConnection> onServerConnect;
    public static event Action<NetworkConnection> onClientConnect;
    private int numOfPlayers;

    // Return Network Discovery component
    public static LanController Discovery
    {
        get
        {
            return singleton.GetComponent<LanController>();
        }
    }

    // Return Match component or create one if it does not exists
    public static NetworkMatch Match
    {
        get
        {
            return singleton.GetComponent<NetworkMatch>() ?? singleton.gameObject.AddComponent<NetworkMatch>();
        }
    }

    // public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    // {
    //     base.OnServerAddPlayer(conn, playerControllerId)
    //     Transform spawnLocation =  
    //     GameObject player = (GameObject)Instantiate(playerPrefab, , Quaternion.identity);
    //     NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    // }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        if (!conn.address.Equals(SystemConstants.NETWORK_NAME_LOCAL_CLIENT))
        {
            onServerConnect?.Invoke(conn);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        if (!GameManager.instance.gameOverDisconnection) GameManager.instance.restartWithNetworkError();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        if (!GameManager.instance.gameOverDisconnection) GameManager.instance.restartWithNetworkError();
    }
}
