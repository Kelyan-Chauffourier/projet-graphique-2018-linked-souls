using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Trigger : NetworkBehaviour {
	protected List<ActivatedObjectCondition> liste;

    [SyncVar]
	protected bool active;
	//public TriggerActionLink[] listTriggers;

	protected virtual void Awake(){
		liste = new List<ActivatedObjectCondition>();
	}

	public virtual void setActive(bool Active){
		this.active = Active;
		OnTriggerStateChange ();
	}

	public virtual void OnInteract(){
	}

	public virtual bool isActive(){
		return active;
	}

	protected void OnTriggerStateChange(){
		for (int i = 0; i < liste.Count; i++) {
			liste [i].OnTriggerChange ();
		}
	}

	public virtual void addActivated(ActivatedObjectCondition obj){
		Debug.Log (liste);
		liste.Add (obj);
	}
}
