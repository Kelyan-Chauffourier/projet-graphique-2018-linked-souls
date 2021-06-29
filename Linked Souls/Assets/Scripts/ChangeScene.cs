using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ChangeScene : NetworkBehaviour
{
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Stairs");
		if(other.tag == "Player")
		{
            GameObject networkmanager = GameObject.Find("NetworkManager");
            networkmanager.GetComponent<NetworkManager>().ServerChangeScene("Dungeon");
		}
	}
}
