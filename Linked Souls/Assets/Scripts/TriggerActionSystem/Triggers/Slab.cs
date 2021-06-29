using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slab : Trigger {
	private int playerCount;

	protected override void Awake(){
		base.Awake ();
	}

	public void OnTriggerEnter2D (Collider2D target) {
		if(target.CompareTag("Player")) {
			playerCount++;
			setActive (playerCount >0);
		}
	}

	public void OnTriggerExit2D (Collider2D target) {
		if (target.CompareTag ("Player")) {
			Debug.Log ("sortie trigger");
			playerCount--;
			setActive (playerCount >0);
		}
	}
}
