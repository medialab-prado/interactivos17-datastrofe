using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	public float time;
	public TimeSpan currenttime;
	public Transform SunTransform;
	public Light sun;
	public Text timetext;
	public int days;

	public float intensity;
	public Color fogday = Color.grey;
	public Color fognight = Color.black;
	public Color day = Color.white;
	public Color night = new Color(0.1271299f, 0, 0.8014706f);

	public int speed;

	public void ChangeTime() {
		time = System.DateTime.Now.Hour*60*60 + System.DateTime.Now.Minute*60 + System.DateTime.Now.Second;
		//time = Time.deltaTime * speed;
		if (time>86400) {
			days++;
			time = 0;
		}
		currenttime = TimeSpan.FromSeconds(time);
		string[] temptime = currenttime.ToString().Split(":"[0]);
		//timetext.text = temptime[0] + ":" + temptime[1];
		SunTransform.rotation = Quaternion.Euler(new Vector3((time-21600)/86400*360,(time-21600)/86400*360,0));
		if (time<43200) {
			intensity = 1 - (43200 - time) / 43200;
		} else {
			intensity = 1 - ((43200 - time) / 43200 * -1);
		}

		RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity *  intensity);
		RenderSettings.ambientSkyColor = Color.Lerp(day, night, intensity * intensity);
		sun.intensity = intensity;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeTime();
	}
}
