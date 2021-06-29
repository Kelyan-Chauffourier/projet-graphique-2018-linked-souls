using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActivatedObjectCondition : NetworkBehaviour {

    [SyncVar]
	protected bool active;
	public Trigger[] listeTriggers;
	protected ActivatedObjectMaster master;

	protected virtual void Awake(){
		master = this.gameObject.GetComponent<ActivatedObjectMaster> ();
	}

	protected virtual void Start(){
		for (int i = 0; i < listeTriggers.Length; i++) {
			listeTriggers [i].addActivated (this);
		}
	}

	public virtual void setActive(bool isActive){
		if (isActive != active) {
			active = isActive;
			master.OnConditionChange ();
		}
	}

	public virtual void OnTriggerChange(){
		Debug.Log("OnTriggerChange");
		bool allActive = true;
		for (int i = 0; i < listeTriggers.Length; i++) {
			allActive = allActive && listeTriggers [i].isActive ();
		}
		setActive (allActive);

	}

	public virtual bool isActive(){
		return active ;
	}
}
