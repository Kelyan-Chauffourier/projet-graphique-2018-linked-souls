using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SimpleBarExample {

	public class ManaBar : MonoBehaviour {

		public float dampening = 5f;

		Material mat;
		float fillTarget = .5f;
		float delta = 0f;

		void Awake() {
			
			Renderer rend = GetComponent<Renderer>();
			Image img = GetComponent<Image>();
			if (rend != null) {
				mat = new Material(rend.material);
				rend.material = mat;
			} else if (img != null) {
				mat = new Material(img.material);
				img.material = mat;
			} else {
				Debug.LogWarning("No Renderer or Image attached to " + name);
			}


		}

		void Update() {

			if (GameObject.Find("Player(Clone)"))
			{
				float newFill = GameObject.Find("Player(Clone)").GetComponent<PlayerController>().manaPoints / 100f;


				delta -= fillTarget - newFill;
				fillTarget = newFill;

				delta = Mathf.Lerp(delta, 0, Time.deltaTime * dampening);

				mat.SetFloat("_Delta", delta);
				mat.SetFloat("_Fill", fillTarget);
			}
			
		}

	}
}
