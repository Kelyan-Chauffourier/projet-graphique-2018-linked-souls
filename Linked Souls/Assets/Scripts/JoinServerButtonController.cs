using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinServerButtonController : MonoBehaviour {

	public MatchInfoSnapshot match;
	public NetworkManagerController controller;

	// Use this for initialization
	void Start () {
		Button btn = this.gameObject.GetComponent<Button>();
		btn.onClick.AddListener (OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClick(){
		controller.JoinMatch (match);
	}
}
