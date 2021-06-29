using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public static class EventManager {

	private static Dictionary<string, UnityEvent> m_Events = new Dictionary<string, UnityEvent> ();

	public static void AddListener(string a_EventName, UnityAction a_Action){
		UnityEvent thisEvent = null;

		bool t_EventExists = m_Events.TryGetValue (a_EventName, out thisEvent);

		if (!t_EventExists) {
			thisEvent = new UnityEvent ();
			m_Events.Add (a_EventName, thisEvent);
		}
		thisEvent.AddListener(a_Action);
	}

	public static void RemoveListener(string a_EventName, UnityAction a_Action){
		UnityEvent thisEvent = null;

		bool t_EventExists = m_Events.TryGetValue (a_EventName, out thisEvent);
		if (t_EventExists) {
			thisEvent.RemoveListener (a_Action);
		} 
		else {
			Debug.Log("EventManager : Event " + a_EventName + " does not exist");
		}

	}

	public static void TriggerListener(string a_EventName){
		UnityEvent thisEvent = null;

		bool t_EventExists = m_Events.TryGetValue (a_EventName, out thisEvent);
		if (t_EventExists) {
			thisEvent.Invoke ();
		} 
		else {
			Debug.Log ("EventManager : Event " + a_EventName + " does not exist");
		}
	}

	private static void CreateEventIfNotExists(UnityEvent a_UnityEvent) {
		if (a_UnityEvent == null) {
			a_UnityEvent = new UnityEvent();
		}
	}
}
