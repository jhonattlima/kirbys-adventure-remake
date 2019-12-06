using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoredData
{
    public string fromAddress;
    public string id;
    public string data;
    public float expiration;

    public StoredData(string fromAddress, string id, string data)
    {
        this.fromAddress = fromAddress;
        this.id = id;
        this.data = data;
    }
}

public class LanController : NetworkDiscovery
{
    // Variables
    private StoredData[] storedDatas;
    private bool isBroadcasting = false;
    public static LanController instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        //DontDestroyOnLoad(this);
    }

    public bool ConfirmDiscoveryStopped()
    {
        bool stopConfirmed = false;
        try
        {
            stopConfirmed = !NetworkTransport.IsBroadcastDiscoveryRunning();
        }
        catch
        {
            stopConfirmed = true;
        }
        return stopConfirmed;
    }

    public void listenMatches()
    {
        storedDatas = new StoredData[SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING];
        base.Initialize();
        base.StartAsClient();
        isBroadcasting = true;
        StartCoroutine(recycleMatches());
        Debug.Log("Lan Controller: Started to listen matches.");
    }

    public void createMatch(string name)
    {
        turnOffBroadcast();
        StartCoroutine(waitForBroadcastToBeturnedOff(name));
    }

    IEnumerator waitForBroadcastToBeturnedOff(string name)
    {
        yield return new WaitUntil(() => ConfirmDiscoveryStopped());
        base.broadcastData = SystemConstants.NETWORK_BROADCAST_IDENTIFIER + "/" + name + "/" + Random.Range(1, 1000);
        StartAsServer();
        isBroadcasting = true;
        NetworkController.singleton.StartHost();
        Debug.Log("Lan Controller: Created match with name " + name);
    }

    public void enterOnMatch(ButtonMatchController button)
    {
        turnOffBroadcast();
        StopCoroutine(recycleMatches());
        NetworkController.singleton.networkAddress = button.lanMatch.fromAddress;
        NetworkController.singleton.StartClient();
        Debug.Log("Lan Controller: New player entered on match.");
    }

    public void cancelLanDiscovery()
    {
        turnOffBroadcast();
        StopCoroutine(this.recycleMatches());
        Debug.Log("Lan Controller: Cancelled Lan Mode.");
    }

    public void turnOffBroadcast()
    {
        try
        {
            StopBroadcast();
            isBroadcasting = false;
        }
        catch { }
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);
        bool changed = false;
        string[] splitData = data.Split('/');
        //if(!fromAddress.Contains("10.")) return; // Turn on on PUCRS
        Debug.Log(data);
        if (splitData[0].Equals(SystemConstants.NETWORK_BROADCAST_IDENTIFIER))
        {
            StoredData newReceivedBroadcast = new StoredData(fromAddress, splitData[splitData.Length - 1], splitData[1]);
            for (int i = 0; i < SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING; i++)
            {
                if (storedDatas[i] != null && storedDatas[i].id.Equals(newReceivedBroadcast.id))
                {
                    storedDatas[i].expiration = Time.time + SystemConstants.NETWORK_TIME_TO_REFRESH_ONLINE_MATCHES;
                    break;
                }
                else if (storedDatas[i] == null)
                {
                    newReceivedBroadcast.expiration = Time.time + SystemConstants.NETWORK_TIME_TO_REFRESH_ONLINE_MATCHES;
                    storedDatas[i] = newReceivedBroadcast;
                    changed = true;
                    break;
                }
            }
            Debug.Log("Lan Controller: Received valid broadcast: " + data);
            if (changed) UIPanelMainMenuController.instance.updateMatchButtons(storedDatas);
        }
    }

    IEnumerator recycleMatches()
    {
        while (true)
        {
            bool changed = false;
            for (int i = 0; i < SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING; i++)
            {
                if (storedDatas[i] != null && storedDatas[i].expiration <= Time.time)
                {
                    storedDatas[i] = null;
                    changed = true;
                }
            }
            if (changed) UIPanelMainMenuController.instance.updateMatchButtons(storedDatas);
            yield return new WaitForSeconds(SystemConstants.NETWORK_TIME_TO_REFRESH_ONLINE_MATCHES);
        }
    }
}
