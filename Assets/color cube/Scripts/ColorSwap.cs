using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour {

	public Color color;
	private AudioSource changeSound;

	void Start() {
		color = new Color (0, 0.7f, 1, 1);
		changeSound = GameObject.Find("colorChangeSound").GetComponent<AudioSource> ();
		Renderer[] kocke = GetComponentsInChildren<Renderer> ();
		for (int i = 0; i < kocke.Length; i++) {
			kocke[i].material.SetColor ("_Color", color);
		}
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) { 
				if (hit.collider.tag.Equals ("box")) {
					PlayerPrefs.SetInt("colorChange", PlayerPrefs.GetInt("colorChange") + 1);
					changeSound.Play();
					Renderer rend = hit.collider.gameObject.GetComponent<Renderer>();
					if (rend.material.GetColor ("_Color").Equals(color)) {
						hit.collider.isTrigger = true;
						hit.collider.GetComponent<CubeController>().isEmpty = true;
						rend.material.SetColor ("_Color", new Color (1, 1, 1, 0.7f));
					} else {
						hit.collider.isTrigger = false;
						hit.collider.GetComponent<CubeController>().isEmpty = false;
						rend.material.SetColor ("_Color", color);
					}
				}
			}
		}
	}
		
}
