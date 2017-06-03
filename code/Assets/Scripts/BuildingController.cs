using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

	private Light[] lights;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		lights = GetComponentsInChildren<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameConfig.Instance.nightMode) {
			foreach (Light light in lights) {
				light.enabled = true;
			}
		}
	}
}
