using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioController : MonoBehaviour {


	public AudioSource mainTheme;
	public AudioSource pajarosR;
	public AudioSource pajarosL;
	public AudioSource electroRuido;
	public AudioSource ruidoCrack;
	public AudioSource ruidoTraficoL;
	public AudioSource ruidoTraficoR;
	public AudioSource ruidoVocesL;
	public AudioSource ruidoVocesR;
	public AudioSource ruidoSoftL;
	public AudioSource ruidoSoftR;


	void Awake() {
		mainTheme.volume = 0;
		pajarosL.volume = 0;
		pajarosR.volume = 0;
		electroRuido.volume = 0;
		ruidoCrack.volume = 0;
		ruidoTraficoL.volume = 0;
		ruidoTraficoR.volume = 0;
		ruidoVocesR.volume = 0;
		ruidoVocesL.volume = 0;
		ruidoSoftR.volume = 0;
		ruidoSoftL.volume = 0;
	}

	void Start () {
		mainTheme.volume = 0.8f;
		pajarosR.volume = 0.4f;
		pajarosL.volume = 0.4f;
	}
	
	void Update () {
		//trafico
		if (GameLogic.totalTrafico>0) {
			ruidoTraficoR.volume = Mathf.Lerp(0, 1f, GameLogic.totalRuido / 100f);
			ruidoTraficoL.volume = Mathf.Lerp(0, 1f, GameLogic.totalRuido / 100f);
		}
		//voces
		if (GameLogic.totalRuido>0) { 
			ruidoVocesR.volume = Mathf.Lerp(0, 1f, GameLogic.totalTrafico / 100f);
			ruidoVocesL.volume = Mathf.Lerp(0, 1f, GameLogic.totalTrafico / 100f);
		}
		//soft ruido
		if ((GameLogic.totalTrafico + GameLogic.totalRuido)>0) {
			float t = ((GameLogic.totalTrafico + GameLogic.totalRuido)/2f)/100f;
			ruidoSoftL.volume = Mathf.Lerp(0, 1f, t);
			ruidoSoftR.volume = Mathf.Lerp(0, 1f, t);
		}
		//rudio crack
		if ((GameLogic.totalTrafico + GameLogic.totalContamination)>0) {
			float t = ((GameLogic.totalTrafico + GameLogic.totalContamination)/2f)/100f;
			ruidoCrack.volume = Mathf.Lerp(0, 1f, t);
		}
		//electro
		if ((GameLogic.totalRuido + GameLogic.totalContamination)>0) {
			float t = ((GameLogic.totalRuido + GameLogic.totalContamination)/2f)/100f;
			electroRuido.volume = Mathf.Lerp(0, 1f, t);
		}
		//pajaros && mainTheme
		if ((GameLogic.totalRuido + GameLogic.totalContamination + GameLogic.totalTrafico)>0) {
			float t = 1f - ((GameLogic.totalRuido + GameLogic.totalContamination + GameLogic.totalTrafico)/3f)/100f;
			mainTheme.volume = Mathf.Lerp(0, 1f, 1f - (t*t));
			pajarosL.volume = Mathf.Lerp(0, 0.7f, 0.7f - (t*t));
			pajarosR.volume = Mathf.Lerp(0, 0.7f, 0.7f - (t*t));
		}
	}
}
