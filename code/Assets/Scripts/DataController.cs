using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Xml;
using System;
using System.IO;
using UnityEngine.UI;


public class DataController : MonoBehaviour {

    private string jsonW;
    private JSONNode data;

    public Light sol;

    [Range(0, 23)]
    public int hora;
    private int Hsunrise;
	private int percentLight;
    private int horaActual;
    private float intensidad;
	public Text stats_agentes;
	public Text stats_trafico;
	public Text stats;
	public int capacidadM30 = 15000;

	public float maxNivelDBA = 150;
	public float minNivelDBA = 35;

	public Text cabecera;
	public Text thora;
	public RawImage icoWeather;
	public Text temp;
	public Text humidity;

	public Text rawContaminationData;
	public Text AQI;
	public Text intensidadTrafico;
	public Text nivelDBA;
	public Text tweetZone;

	private string aQILevel;
	private string noiseLevel;
	private string trafficLevel;

	void OnGUI() {
		GUI.contentColor = new Color(0f, 0f, 0f, 0f);
		GUILayout.Label ("Test");
	}

    void Start () {
		/*
		StartCoroutine(getSunrise());

		
		*/

		StartCoroutine(getAgentesContaminantes());
		StartCoroutine(getMeteo());
		StartCoroutine(getAccoustic());
		StartCoroutine(getTweets());
		StartCoroutine(getTrafico());



    }
	IEnumerator getTweets() {
		while (true) {
			GetComponent<Marquee>().enabled = false;
			WWW tweetsJson = new WWW("http://zona4g.com/datastrofe/");
			yield return tweetsJson;
			data = JSON.Parse(tweetsJson.text);
			GetComponent<Marquee>().message = (data["statuses"][0]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][1]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][2]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][3]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][4]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][5]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][6]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][7]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			GetComponent<Marquee>().message += " | " + (data["statuses"][8]["text"]).ToString().Replace("\r", "").Replace("\n", "").Replace("\\r", "").Replace("\n", "");
			//print((data["statuses"][0]["text"]).ToString().Replace("\r", "").Replace("\n", ""));
			GetComponent<Marquee>().enabled = true;

			yield return new WaitForSeconds(300f);
		}
	}

	IEnumerator getAgentesContaminantes() {

		while (true) {
			WWW aciqnData = new WWW("http://api.waqi.info/feed/madrid/?token=62aae63e4d9a88f8c7717fcf4431b9a4cc7c8cb9");
			yield return aciqnData;
			data = JSON.Parse(aciqnData.text);
			stats_agentes.text += "CO: " + data["data"]["iaqi"]["co"]["v"] + "\n";
			stats_agentes.text += "H: " + data["data"]["iaqi"]["h"]["v"] + "\n";
			stats_agentes.text += "NO2: " + data["data"]["iaqi"]["no2"]["v"] + "\n";
			stats_agentes.text += "O3: " + data["data"]["iaqi"]["o3"]["v"] + "\n";
			stats_agentes.text += "P: " + data["data"]["iaqi"]["p"]["v"] + "\n";
			stats_agentes.text += "PM10: " + data["data"]["iaqi"]["pm10"]["v"] + "\n";
			stats_agentes.text += "PM25: " + data["data"]["iaqi"]["pm25"]["v"] + "\n";
			stats_agentes.text += "SO2: " + data["data"]["iaqi"]["so2"]["v"] + "\n";
			stats_agentes.text += "T: " + data["data"]["iaqi"]["t"]["v"] + "\n";
			stats_agentes.text += "U@: " + data["data"]["time"]["s"] + "\n";
			stats_agentes.text += "AQI: " + data["data"]["aqi"] + "\n";

			GameLogic.contaminacion = (int)data["data"]["aqi"];

			aQILevel = data["data"]["aqi"];

			rawContaminationData.text = 
				"CO: " + data["data"]["iaqi"]["co"]["v"] + " | " + "H: " + data["data"]["iaqi"]["h"]["v"] + " | " + "NO2: " + data["data"]["iaqi"]["no2"]["v"] + " | " + "O3: " + data["data"]["iaqi"]["o3"]["v"] + " | " + "P: " + data["data"]["iaqi"]["p"]["v"] + " | " + "PM10: " + data["data"]["iaqi"]["pm10"]["v"] + " | " + "PM25: " + data["data"]["iaqi"]["pm25"]["v"] + " | " + "SO2: " + data["data"]["iaqi"]["so2"]["v"] + " | " + "T: " + data["data"]["iaqi"]["t"]["v"];
			yield return new WaitForSeconds(3700f);
		}

	}

	IEnumerator getMeteo() {

		while (true) {
			WWW meteo = new WWW("http://api.wunderground.com/api/aa549352e5d8550d/conditions/q/ES/Madrid.json");
			yield return meteo;
			var meteoData = meteo.text;
			data = JSON.Parse(meteoData);
			//string icon = ("https://icons.wxug.com/i/c/j/" + data[1]["weather"] + ".gif").ToLower();
			string icon = (data[1]["icon"]);

			/*WWW iconMeteo = new WWW(icon);
			yield return iconMeteo;*/
			if (System.DateTime.Now.Hour>20 && System.DateTime.Now.Minute > 45) {
				GetComponentInParent<AmbientController>().nightMode = true;
				icon = "nt_" + icon.ToLower();
			} else {
				GetComponentInParent<AmbientController>().nightMode = false;
			}

			Texture2D _icoWeather = (Texture2D)Resources.Load("pngs/"+icon.ToLower());

			icoWeather.texture = _icoWeather;
			temp.text = (data[1]["temp_c"]) + "ºC HR " + (data[1]["relative_humidity"]);
			//humidity.text = (data[1]["temp_c"]) + " ºC";

			var precipitaciones = float.Parse(data[1]["precip_1hr_in"]);

			if (precipitaciones>0) {
				GetComponentInParent<AmbientController>().setRain(true);
			} else {
				GetComponentInParent<AmbientController>().setRain(false);
			}

			print("P " + precipitaciones);
			print("I " + icon);
			print("W " + float.Parse(data[1]["wind_kph"]));
			GetComponentInParent<AmbientController>().setWind(float.Parse(data[1]["wind_kph"]));

			cabecera.text = "MADRID - " + System.DateTime.Now.ToString("dd/MM/yyyy");
			thora.text = System.DateTime.Now.ToString("t") + "h";

			yield return new WaitForSeconds(60f);
		}
		/*yield return StartCoroutine(UniGif.GetTextureListCoroutine(iconMeteo.bytes, (gifTexList, loopCount, width, height) => { 
			GameObject.Find("icoWeather").GetComponent<RawImage>().texture = gifTexList[0].m_texture2d;
		}));*/
	}


	IEnumerator getSunrise() {
		
        WWW getSunrise = new WWW("http://api.wunderground.com/api/aa549352e5d8550d/astronomy/q/ES/Madrid.json");
		yield return getSunrise;        
        jsonW = getSunrise.text;
        
		data = JSON.Parse(jsonW);
		Hsunrise = int.Parse(String.Concat(data["moon_phase"]["sunset"]["hour"], data["moon_phase"]["sunset"]["minute"]));
		percentLight = int.Parse(data["moon_phase"]["percentIlluminated"]);
		stats.text += "SUNSET: " + data["moon_phase"]["sunset"]["hour"] + ":" +data["moon_phase"]["sunset"]["minute"] + "h\n";

		while(true) {
			horaActual = int.Parse(String.Concat(System.DateTime.Now.Hour, System.DateTime.Now.Minute.ToString("D2")));
			hora = horaActual;
			intensidad = 12f - ((horaActual * 12f) / Hsunrise);
			stats.text += "LIGHT INTENSITY CALCULATED: " + intensidad + " (w " + horaActual + ")\n";
			updateLight(intensidad);
			intensidad = (5f*percentLight)/100f;
			stats.text += "% ILLUMINATED " + percentLight + "%\n";
			stats.text += "AMBIENT INTENSITY CALCULATED: " + intensidad + " (w " + horaActual + ")\n";
			updateAmbient(intensidad);
			yield return new WaitForSeconds(60f);
		}
	}

	IEnumerator getAccoustic() {
		WWW getAcustic = new WWW("http://www.mambiente.munimadrid.es/opendata/ruido.txt");
		yield return getAcustic;
		String[] lineas = getAcustic.text.Split('\n');

		float[] datosT = new float[40];
		float[] datosD = new float[40];
		float[] datosE = new float[40];
		float[] datosN = new float[40];

		float totalDatosT = 0;
		float totalDatosD = 0;
		float totalDatosE = 0;
		float totalDatosN = 0;

		var t = 0;
		var d = 0;
		var e = 0;
		var n = 0;
		foreach (var item in lineas) {
			
			var lineaData = item.Split(',');
			if (lineaData.Length<=5) continue;

			var key = lineaData[4].Trim();
			//print(key);
			switch(key) {
				case "T":
					datosT[t] = float.Parse(lineaData[5]);
					totalDatosT += datosT[t];
					t++;
					//Array.Resize(ref datosT, i+1);
					break;
				case "D":
					datosD[d] = float.Parse(lineaData[5]);	
					totalDatosD += datosD[d];
					d++;
					//Array.Resize(ref datosD, i+1);
					break;
				case "E":
					datosE[e] = float.Parse(lineaData[5]);	
					totalDatosE += datosE[e];
					e++;
					//Array.Resize(ref datosE, i+1);
					break;
				case "N":
					datosN[n] = float.Parse(lineaData[5]);	
					totalDatosN += datosN[n];
					n++;
					//Array.Resize(ref datosN, i+1);
					break;
			}
				
		}

		while (true) {
			int hora = System.DateTime.Now.Hour;

			float v = 0;
			print("HORA " + hora);
			if (hora>=7 && hora<13) {
				//print("D " + d);
				v = (totalDatosD / (d-1));
			} else if (hora>=13 && hora<20) {
				//print("E " + e);
				v = (totalDatosE / (e-1));
			} else if (hora>=20 || hora>=0) {
				print("N " + n);
				v = (totalDatosN / (n-1));
			}
			print("HORA " + hora);

			/*print("D " + totalDatosD);
			print("E " + totalDatosE);
			print("N " + totalDatosN);*/
			GameLogic.ruido = (v*100f)/maxNivelDBA;
			GameLogic.rawRuido = v;
			noiseLevel = v.ToString("##");

			yield return new WaitForSeconds(3700f);
		}
	}

	IEnumerator getTrafico() {
		while (true) {
			WWW getTrafico = new WWW("http://www.mc30.es/images/xml/DatosTrafico.xml");
			yield return getTrafico;
			string traficoData = getTrafico.text;

			XmlDocument trafficDoc = new XmlDocument();
			trafficDoc.LoadXml(traficoData);
			XmlNodeList root = trafficDoc.DocumentElement.SelectNodes("DatosTrafico");
			GetComponent<MarqueeTraffic>().message = "";
			root = trafficDoc.DocumentElement.SelectNodes("DatoGlobal");
			foreach(XmlNode t in root) {
				if (t.SelectSingleNode("Nombre").InnerText == "CortesImportantesWeb") continue;
					
				if (t.SelectSingleNode("Nombre").InnerText == "totalVehiculosCalle30") {
					GameLogic.trafico = (int.Parse(t.SelectSingleNode("VALOR").InnerText)*100)/capacidadM30;
					GameLogic.vTrafico =  (t.SelectSingleNode("VALOR").InnerText);
					trafficLevel = t.SelectSingleNode("VALOR").InnerText.ToString();
				}
				stats_trafico.text += (t.SelectSingleNode("Nombre").InnerText + " : " + t.SelectSingleNode("VALOR").InnerText ) + "\n";

			}

			root = trafficDoc.DocumentElement.SelectNodes("DatoTrafico");
			foreach(XmlNode t in root) {
				try  {
					GetComponentInParent<MarqueeTraffic>().message += t.SelectSingleNode("Retenciones").SelectSingleNode("VALOR").InnerText + " | ";
				
				} catch (Exception e) {
					
				}
				try  {
					GetComponentInParent<MarqueeTraffic>().message +=  t.SelectSingleNode("TraficoLento").SelectSingleNode("VALOR").InnerText;
				} catch (Exception e) {

				}
			}
			print("VTRAFICO " + GameLogic.vTrafico);
			GetComponentInParent<TrafficController>().totalVehicles = int.Parse(GameLogic.vTrafico);
			GetComponentInParent<TrafficController>().enabled = true;
			yield return new WaitForSeconds(400f);
		}




	}

	void Update () {
		//sol.transform.Translate(Vector3.down*Time.deltaTime);
		sol.transform.Translate(new Vector3(-0.1f*Time.deltaTime, -40f*Time.deltaTime, 0));
		intensidadTrafico.text = trafficLevel + " (<color=red>" + GameLogic.modTrafico.ToString("+0.00;-#.##") + "%</color>)";
		nivelDBA.text = noiseLevel + "dBA (<color=red>" + GameLogic.modRuido.ToString("+0.00;-#.##") + "%</color>)";
		AQI.text = "AQI " + aQILevel + " (<color=red>" + GameLogic.modContaminacion.ToString("+0.00;-#.##") + "%</color>)";


	}

    private void updateLight(float intensity)
    {
        /*if (intensidad < 0) intensidad = 0;
        sol.intensity = intensity;*/
		//sol.shadowStrength = 0;
		//sol.transform.Translate(new Vector3(40f*Time.deltaTime, 0f, -40f*Time.deltaTime));
	

    }

	private void updateAmbient(float intensity)
	{
		/*if (intensidad < 0) intensidad = 0;
		RenderSettings.ambientIntensity = intensity;

		if (sol.transform.position.y<-400){
			GameConfig.Instance.nightMode = true;
		
			sol.shadowStrength = 0;
			RenderSettings.ambientSkyColor = new Color(0.05f, 0.05f, 0.13f);
		}*/

	}
}
