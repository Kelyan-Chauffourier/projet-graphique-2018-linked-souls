using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDezoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main.orthographicSize < 30)
		Camera.main.orthographicSize += (1f *Time.deltaTime);
	}
}
