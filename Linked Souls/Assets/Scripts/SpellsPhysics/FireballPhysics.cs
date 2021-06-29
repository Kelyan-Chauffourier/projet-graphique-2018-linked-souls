using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireballPhysics : MonoBehaviour {

    private Vector3 direction;
	private float speed = 10f;
    public Vector3 destination;

	// Use this for initialization
	void Start () {
		destination.z = 0;
		direction =  destination - this.transform.position;

		float destroyTime = direction.magnitude / speed;
		Destroy(this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += direction.normalized * speed * Time.deltaTime;
		//Debug.Log(direction.normalized);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Ennemy")
		{
			Destroy(this);
			Destroy(other.gameObject);
		}
	}

}
