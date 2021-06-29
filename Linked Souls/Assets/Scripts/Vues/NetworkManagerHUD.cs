using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerHUD : MonoBehaviour {
	public NetworkManagerController controller = null;
    public GameObject canvasMenuPrincipal;
	public GameObject canvasMenuNetwork;
	public GameObject servButtonPrefab;

    // Use this for initialization
    void Start () {
		controller.StartMatchmaking ();
		EventManager.AddListener ("matchlist", refreshMatches);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
	}

	void OnDisable(){
	}

	public void OnButtonExitClicked()
	{
        canvasMenuPrincipal.SetActive(true);
        canvasMenuNetwork.SetActive(false);
    }

	public void OnButtonRefreshClicked()
	{
		controller.FetchServerList (); 
	}

	public void OnButtonJoinClicked()
	{
		controller.FetchServerList (); 
	}

	public void OnButtonHostClicked()
	{
		string name = GameObject.Find ("GameName").transform.GetChild(2).GetComponent<Text> ().text;
		controller.CreateMatch (name);
		Debug.Log ("CreateMatch");
		/*controller.CreateMatch ("partie");*/
		canvasMenuNetwork.SetActive (false);
	}

	public void refreshMatches()
	{
		Debug.Log("A list of matches was returned");
		GameObject gamesList = null; 
		for (int i = 0; i < canvasMenuNetwork.transform.childCount; i++) {
			if (canvasMenuNetwork.transform.GetChild (i).name == "List") {
				Debug.Log ("List found");
				GameObject List = canvasMenuNetwork.transform.GetChild (i).gameObject;
				for (int j = 0; j < List.transform.childCount; j++) {
					if (List.transform.GetChild (j).name == "Viewport") {
						Debug.Log ("Viewport found");
						gamesList = List.transform.GetChild (j).transform.GetChild (0).gameObject;
						j = List.transform.childCount;
					}
				}
				i = canvasMenuNetwork.transform.childCount;
			}
		}
		for (int i = 0; i < gamesList.transform.childCount; i++) {
			Destroy (gamesList.transform.GetChild (i).gameObject);
		}
		for (int i = 0; i < controller.matches.Count; i++) {
			Debug.Log ("Adding button");
			GameObject btn = Instantiate (servButtonPrefab);
			btn.GetComponent<JoinServerButtonController> ().match = controller.matches [i];
			btn.GetComponent<JoinServerButtonController> ().controller = controller;
			btn.transform.SetParent (gamesList.transform);
			btn.transform.GetComponentInChildren<Text>().text = controller.matches [i].name;
		}
	}
}
