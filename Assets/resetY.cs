using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetY : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 scrPoint = new Vector3(transform.position.x, transform.position.y, 0);
		Ray ray = Camera.main.ScreenPointToRay(scrPoint);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)) {
			Vector3 hitPoint = hit.point;
			transform.position = hitPoint;
		}


	}
}
