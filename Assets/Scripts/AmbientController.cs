using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class AmbientController : MonoBehaviour {

	private static AmbientController instance;
	public Camera mainCamera;

	public static AmbientController Instance {
		get { return instance ?? (instance = (new GameObject("AmbientController").AddComponent<AmbientController>())); }
	}

	public bool nightMode = false;
	public bool rain = false;
	public ParticleSystem lluvia;
	public GameObject nubes;
	public float windvel;


	// Use this for initialization
	void Start () {
		setRain(false);	
		mainCamera.GetComponent<NoiseAndScratches>().enabled = true;
		mainCamera.GetComponent<NoiseAndScratches>().monochrome = true;
		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMin = 0;
		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMax = 0;
		mainCamera.GetComponent<NoiseAndScratches>().grainSize = 2;
	}
	
	// Update is called once per frame
	void Update () {
		float totalNoiseAndTrafficMin = -1 + (((GameLogic.totalRuido + GameLogic.totalTrafico)/2f)*4)/100f;
		float totalNoiseAndTrafficMax = -1 + (((GameLogic.totalRuido + GameLogic.totalTrafico)/2f)*3)/100f;
		float totalNoise = -1 + (((GameLogic.totalContamination + GameLogic.totalRuido + GameLogic.totalTrafico)/3f)*3)/100f;
		/*
		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMax = totalNoiseAndTrafficMax;
		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMin = totalNoiseAndTrafficMin;

		float totalContamination = (GameLogic.totalContamination*6f)/100f;
		mainCamera.GetComponent<NoiseAndScratches>().scratchIntensityMax = totalContamination;
		mainCamera.GetComponent<NoiseAndScratches>().scratchIntensityMin = totalContamination;
		*/

		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMax = totalNoise;
		mainCamera.GetComponent<NoiseAndScratches>().grainIntensityMin = totalNoise;
		mainCamera.GetComponent<NoiseAndScratches>().scratchIntensityMax = totalNoise;
		mainCamera.GetComponent<NoiseAndScratches>().scratchIntensityMin = totalNoise;

		//print("nightMode " + nightMode);
		if (nightMode) {
			nubes.SetActive(false);
		} else {
			nubes.SetActive(true);
			moverNubes();
		}
	}

	public void setNightMode() {
					
	}

	public void setRain(bool start) {
		print("A LLOVER  " + start);
		if (start) {
			rain = true;
			lluvia.gameObject.SetActive(true);
			lluvia.Play();
		} else {
			rain = false;
			lluvia.gameObject.SetActive(false);
			lluvia.Stop();
		}
	}

	public void setWind(float wv) {
		windvel = wv;
	}

	public void moverNubes() {
		//var objNubes = nubes.GetComponentsInChildren<GameObject>();
		if (windvel<=0) windvel = 0.1f;
		foreach (Transform child in nubes.transform) {
			child.gameObject.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,0.2f);
			child.Translate(0,0,-1*(windvel/3f)*Time.deltaTime, Space.World);
		}
	}

}
