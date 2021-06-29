using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ActivatedObjectMaster : NetworkBehaviour {

    [SyncVar]
	protected bool active;

	protected ActivatedObjectCondition[] conditionsList;

	protected virtual void Awake() {
		conditionsList = this.gameObject.GetComponents<ActivatedObjectCondition>();
	}

	public virtual void setActive(bool isActive){
		active = isActive;
	}

	public virtual void OnConditionChange(){
		bool isActive = false;
		for (int i = 0; i < conditionsList.Length; i++) {
            isActive = isActive || conditionsList [i].isActive();
			Debug.Log (isActive);
		}
		active = isActive;
	}
}
