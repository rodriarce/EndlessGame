using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	void Start () {
		Renderer[] renderer = GetComponentsInChildren<Renderer> ();
		BoxCollider[] cubeCollider = GetComponentsInChildren<BoxCollider> ();
		for (int i = 0; i < renderer.Length; i++) {
			int rand = Random.Range (0, 2);
			if (rand == 0) {
				renderer [i].material.SetColor ("_Color", new Color (0, 0.7f, 1, 1));
				cubeCollider [i].isTrigger = true;
			} else {
				renderer [i].material.SetColor ("_Color", new Color (1, 1, 1, 0.7f));
				cubeCollider [i].isTrigger = false;
			}
		}
	}
}
