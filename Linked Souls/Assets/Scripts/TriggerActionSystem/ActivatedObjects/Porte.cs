using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Porte : ActivatedObjectMaster {
	public GameObject target;
	protected GameObject posOrigin;
	public GameObject posEnd;
	public float activespeed;
	public float inactivespeed;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		posOrigin = new GameObject() ;
        posOrigin.transform.position = target.transform.position;
		posOrigin.transform.rotation = target.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
		if (active) {
			if (target.transform.position != posEnd.transform.position)
				target.transform.position = Vector3.Lerp (target.transform.position, posEnd.transform.position, 
					(activespeed/Vector2.Distance(target.transform.position, posEnd.transform.position)) * Time.deltaTime);
		} else {
			if(target.transform.position != posOrigin.transform.position)
				target.transform.position = Vector3.Lerp (target.transform.position, posOrigin.transform.position, activespeed * Time.deltaTime);
		}
        /*if (PorteLevante.isDalle1 == true && PorteLevante.isDalle2 == true)
        {
            if (PorteLevante.canOpen)
            {
                PorteLevante.MonterPorte();
                PorteLevante.canOpen = false;
            }
            else
            {
                PorteLevante.canClose = true;
                Debug.Log("Porte deja monté");
            }

        }
        else if(PorteLevante.isDalle1 == false | PorteLevante.isDalle2 == false)
        {
            if (PorteLevante.canClose)
            {
                PorteLevante.LowerPorte(porteOrigin);
                PorteLevante.canClose = false;
            }
            else
            {
                PorteLevante.canOpen = true;
               // Debug.Log("Porte deja descendu");
            }
        }*/
    }

	public override void setActive(bool isActive){
		Debug.Log ("setting door active");
		active = isActive;
	}
}
