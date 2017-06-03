using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeController : MonoBehaviour {

	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (System.DateTime.Now.Hour == 1 && System.DateTime.Now.Minute == 48) {
			transform.position = originalPosition;
		}
	}
}
