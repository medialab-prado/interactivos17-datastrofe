﻿using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour {


	void Awake() {
	}

	void OnEnable () {
		transform.SetAsLastSibling ();
	}
}