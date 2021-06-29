using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class NetworkManagerController : MonoBehaviour {
	public List<MatchInfoSnapshot> matches;
	public GameObject canvasMenuNetwork;

	public void StartMatchmaking(){
		Debug.Log ("Starting Matchmaker");
		NetworkManager.singleton.StartMatchMaker ();
		Debug.Log ("Matchmaker Started");
	}

	public void StopMatchmaking(){
		Debug.Log ("Stopping Matchmaker");
		NetworkManager.singleton.StopMatchMaker ();
	}

	public void CreateMatch(string matchName = "partie"){
		Debug.Log ("Creating match");
		NetworkManager.singleton.matchMaker.CreateMatch (matchName, 2, true, "", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
	}

	public void FetchServerList(){
		NetworkManager.singleton.matchMaker.ListMatches (0, 10, "", true, 0, 0, OnInternetMatchList);
		Debug.Log ("listing matches");
	}

	// Update is called once per frame
	void Update () {

	}

	public void JoinMatch(MatchInfoSnapshot match){
		NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
		canvasMenuNetwork.SetActive (false);
	}

	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> listematches)
	{
		if (success)
		{
			matches = listematches;
			if (listematches.Count != 0)
			{
				EventManager.TriggerListener("matchlist");
			}
			else
			{
				Debug.Log("No matches in requested room!");
			}
		}
		else
		{
			Debug.LogError("Couldn't connect to match maker");
		}
	}
}
