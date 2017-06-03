using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChoque : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (GetComponentInParent<FollowPath>().actualSpeed<GetComponentInParent<FollowPath>().speed) {
			GetComponentInParent<FollowPath>().actualSpeed+=5f;
		}
		if (GetComponentInParent<FollowPath>().actualSpeed>GetComponentInParent<FollowPath>().speed) {
			GetComponentInParent<FollowPath>().actualSpeed = GetComponentInParent<FollowPath>().speed;
		}*/
	}

	/*
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "coche") {
			GetComponentInParent<FollowPath>().actualSpeed = other.gameObject.GetComponent<FollowPath>().actualSpeed - 20f;
			if (GetComponentInParent<FollowPath>().actualSpeed<0) GetComponentInParent<FollowPath>().actualSpeed = 0;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "coche") {
			GetComponentInParent<FollowPath>().actualSpeed -= 25f;
			if (GetComponentInParent<FollowPath>().actualSpeed<0) GetComponentInParent<FollowPath>().actualSpeed = 0;
		}
	}*/
}
