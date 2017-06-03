using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	public Light sun;
	public Light pointSun;

	public float secondsInFullDay = 120f;
	[Range(0,1)]
	public float currentTimeOfDay = 0;
	[HideInInspector]
	public float timeMultiplier = 1f;

	float sunInitialIntensity;

	void Awake() {
		
	}

	void Start() {
		sunInitialIntensity = sun.intensity;
	}

	void Update() {
		UpdateSun();

		var seconds = System.DateTime.Now.Hour*60*60 + System.DateTime.Now.Minute*60 + System.DateTime.Now.Second;

		currentTimeOfDay = ((seconds / secondsInFullDay) * timeMultiplier);
		/*
		print(seconds + "-->" + currentTimeOfDay + " OF " + secondsInFullDay + " * " + timeMultiplier);
		print(seconds + "/" + secondsInFullDay);
		*/
		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
		}
	}

	void UpdateSun() {
		sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
		pointSun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * -360f), 170, 0);
		float intensityMultiplier = 1;
		if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f) {
			intensityMultiplier = 0;
			RenderSettings.ambientSkyColor = new Color(0, 0.000862062f, 0.125f);
			RenderSettings.ambientEquatorColor = Color.black;
			RenderSettings.ambientGroundColor = Color.black;
			GetComponentInParent<AmbientController>().nightMode = true;

		}
		else if (currentTimeOfDay <= 0.25f) {
			intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
			RenderSettings.ambientSkyColor = new Color(0, 0.000862062f, 0.125f);
			RenderSettings.ambientEquatorColor = Color.black;
			RenderSettings.ambientGroundColor = Color.black;
			GetComponentInParent<AmbientController>().nightMode = true;
		}
		else if (currentTimeOfDay >= 0.73f) {
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
			RenderSettings.ambientSkyColor = new Color(0, 0.000862062f, 0.125f);
			RenderSettings.ambientEquatorColor = Color.black;
			RenderSettings.ambientGroundColor = Color.black;
			GetComponentInParent<AmbientController>().nightMode = true;
		} else {
			RenderSettings.ambientSkyColor = Color.white;
			RenderSettings.ambientEquatorColor = Color.white;
			RenderSettings.ambientGroundColor = Color.white;
			GetComponentInParent<AmbientController>().nightMode = false;
		}

		if (GetComponentInParent<AmbientController>() && GetComponentInParent<AmbientController>().rain) {
			sun.intensity = 0.18f;
			RenderSettings.ambientSkyColor = Color.gray;
			RenderSettings.ambientEquatorColor = Color.black;
			RenderSettings.ambientGroundColor = Color.black;
		} else {
			sun.intensity = sunInitialIntensity * intensityMultiplier;
		}

		pointSun.intensity = sun.intensity/intensityMultiplier;
	}
}