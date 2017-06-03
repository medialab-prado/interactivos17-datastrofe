using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPanel : MonoBehaviour {

	public Text contaminacion;
	public Text ruido;
	public Text trafico;
	public Text titulo;
	public Text coste;
	public Text income;
	public Vector3 offset;
	public GameObject tooltipPanelObject;


	//private static TooltipPanel tooltipPanel;
	/*
	private static TooltipPanel instance;

	public static TooltipPanel Instance
	{
		get { return instance ?? (instance = new GameObject("TooltipPanel").AddComponent<TooltipPanel>()); }
	}
	*/
	/*
	public static TooltipPanel Instance () {

		if (!tooltipPanel) {
			tooltipPanel = FindObjectOfType(typeof (TooltipPanel)) as TooltipPanel;
			if (!tooltipPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return tooltipPanel;
	}*/

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(string _titulo,
		string _contaminacion,
		string _ruido,
		string _trafico,
		string _coste,
		string _income,
		Vector3 pos) {
		contaminacion.text = _contaminacion + "%";
		ruido.text = _ruido + "%";
		trafico.text = _trafico + "%";
		titulo.text = _titulo;
		coste.text = _coste;
		income.text = _income;
		tooltipPanelObject.transform.position = pos + offset;
		tooltipPanelObject.SetActive(true); 
	}

	public void Hide() {
		tooltipPanelObject.SetActive(false);
	}
}
