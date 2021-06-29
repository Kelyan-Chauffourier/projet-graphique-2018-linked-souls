using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpView : MonoBehaviour {

	public GameObject canvasMenuPrincipal;
	public GameObject canvasHelpMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnBackButtonPressed()
	{
		canvasMenuPrincipal.SetActive(true);
		canvasHelpMenu.SetActive(false);
	}
}
