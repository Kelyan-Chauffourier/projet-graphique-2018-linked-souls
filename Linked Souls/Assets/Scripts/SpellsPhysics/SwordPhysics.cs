using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPhysics : MonoBehaviour {

	public Transform startPositionAndRotation;
	// Use this for initialization
	void Start ()
	{
		startPositionAndRotation = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
	}


	public void BasicAttack()
	{
		if (gameObject.activeSelf)
		{
			return;
		}
	}
}