using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameView : MonoBehaviour {

	public GameObject canvasInventory;
	public GameObject canvasMap;
	public GameObject canvasCharacteristics;

	private bool inventory = false;
	private bool map = false;
	private bool characteristics = false;


	public void Inventory()
	{
		if (inventory == false)
		{
			inventory = true;
			canvasInventory.SetActive(true);
		}

		else
		{
			inventory = false;
			canvasInventory.SetActive(!true);
		}
	}

	public void Map()
	{
		if (map == false)
		{
			map = true;
			canvasMap.SetActive(true);
		}

		else
		{
			map = false;
			canvasMap.SetActive(!true);
		}
	}

	public void Characteristics()
	{
		if (characteristics == false)
		{
			characteristics = true;
			canvasCharacteristics.SetActive(true);
		}

		else
		{
			characteristics = false;
			canvasCharacteristics.SetActive(!true);
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.I))
			Inventory();


		if (Input.GetKeyDown(KeyCode.C))
			Characteristics();

		if (Input.GetKeyDown(KeyCode.M))
			Map();
	}
}
