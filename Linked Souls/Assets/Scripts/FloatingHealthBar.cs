using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player(Clone)");

    }
	
	// Update is called once per frame
	void Update () {
        
        gameObject.GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().lifePoints / 100;
    }
}
