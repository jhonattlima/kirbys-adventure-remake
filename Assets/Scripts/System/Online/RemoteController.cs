using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

internal class RemoteController : MonoBehaviour
{
    MatchInfoSnapshot[] storedMatches;

    public static RemoteController instance;

    private Coroutine recycleRoutine;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        storedMatches = new MatchInfoSnapshot[SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING];
    }

    public void listenMatches()
    {
        recycleRoutine = StartCoroutine(recycleMatches());
        Debug.Log("Remote Controller: Started to listen matches.");
    }

    public void createMatch(string matchName)
    {
        NetworkController.Match.CreateMatch(name, 2, true, "", "", "", 0, 0, NetworkController.singleton.OnMatchCreate);
        StopCoroutine(recycleRoutine);
        Debug.Log("Remote Controller: Created a match with name " + matchName);
    }

    public void enterOnMatch(ButtonMatchController button)
    {
        StopCoroutine(recycleRoutine);
        NetworkController.Match.JoinMatch(button.remoteMatch.networkId, "", "",
            "", 0, 0, NetworkController.singleton.OnMatchJoined);
        Debug.Log("Remote Controller: Entered on a match with name " + button.remoteMatch.name);
    }

    public void cancelRemoteDiscovery()
    {
        StopCoroutine(recycleRoutine);
        Debug.Log("Remote Controller: Cancelled Remote mode.");
    }

    IEnumerator recycleMatches()
    {
        List<MatchInfoSnapshot> updatedMatches;
        while (true)
        {
            NetworkController.Match.ListMatches(0, SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING, "", true, 0, 0, NetworkController.singleton.OnMatchList);
            updatedMatches = NetworkController.singleton.matches;
            int counter = 0;
            if (updatedMatches != null && updatedMatches.Count > 0)
            {
                foreach (var match in updatedMatches)
                {
                    Debug.Log("Matches found : " + match.name);
                    if (counter < SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING)
                    {
                        storedMatches[counter] = match;
                        counter++;
                    }
                    break;
                }
            }
            while (counter < SystemConstants.NETWORK_MAXIMUM_MATCHES_SHOWING)
            {
                storedMatches[counter] = null;
                counter++;
            }
            UIPanelMainMenuController.instance.updateMatchButtons(storedMatches);
            yield return new WaitForSeconds(SystemConstants.NETWORK_TIME_TO_REFRESH_ONLINE_MATCHES);
        }
    }
}