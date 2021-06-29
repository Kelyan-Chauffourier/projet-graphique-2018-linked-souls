using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoors : ActivatedObjectMaster
{
	public GameObject [] target;
	public float activespeed;

	// Update is called once per frame
	void Update()
	{
		if (active)
		{
			for (int i = 0; i < target.Length; i++)
			{
				target[i].transform.localScale = Vector3.Lerp(target[i].transform.localScale, Vector3.zero, 
					(activespeed / Vector2.Distance(target[i].transform.localScale, Vector3.zero)) * Time.deltaTime);
			}
		}
	}

	public override void setActive(bool isActive)
	{
		Debug.Log("setting door active");
		if(isActive)
			active = 	true;
	}
}
