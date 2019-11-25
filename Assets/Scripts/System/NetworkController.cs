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

    // Return Network Discovery component
    public static NetworkDiscovery Discovery{
        get{
            return singleton.GetComponent<NetworkDiscovery>();
        }
    }

    // Return Match component or create one if it does not exists
    public static NetworkMatch Match{
        get{
            return singleton.GetComponent<NetworkMatch>() ?? singleton.gameObject.AddComponent<NetworkMatch>();
        }
    }

    public override void OnServerConnect(NetworkConnection conn){
        base.OnServerConnect(conn);
        if(!conn.address.Equals(SystemConstants.NETWORK_NAME_LOCAL_CLIENT)){
            onServerConnect?.Invoke(conn);
        }
        //Debug.Log("Number of players: " + numPlayers);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, PrefabsAndInstancesLibrary.instance.spawnSpotPlayer1.transform.position , Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode){
        base.OnClientError(conn, errorCode);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        cancelOnlineMatch();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        cancelOnlineMatch();
    }

    public void startNewOnlineMatch()
    {
        Debug.Log("Entered here");
    }

    public static void cancelOnlineMatch()
    {   try
        {
            if(NetworkController.singleton.matchInfo != null) NetworkController.Match.DestroyMatch(NetworkController.singleton.matchInfo.networkId, 
            NetworkController.singleton.matchInfo.domain, OnMatchDestroy);
            NetworkController.singleton.StopMatchMaker();
            NetworkController.Shutdown();
            // Reload scene;
        } 
        catch (Exception e)
        {
            Debug.Log("Handled Error: " + e.Message);
        }
    }

    public static void OnMatchDestroy(bool success, string extendedInfo)
    {
        // Destroy match dependency
    }
}
