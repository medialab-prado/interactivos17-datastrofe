using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class ShowDialog : MonoBehaviour {
	private ModalPanel modalPanel;
	private static ShowDialog instance;

	/*
	void Awake () {
		modalPanel = ModalPanel.Instance ();
	}*/


	public static ShowDialog Instance
	{
		get { return instance ?? (instance = new GameObject("ShowDialog").AddComponent<ShowDialog>()); }
	}
		
	public void SetImage(Sprite imagen=null) {
		modalPanel.GetComponentInChildren<Image>().sprite = imagen;
	}
		
	public void Show (string question, UnityAction myYesAction, UnityAction myNoAction, UnityAction myCancelAction) {
		modalPanel.Choice (question, myYesAction, myNoAction, myCancelAction);
	}

	public void Show (string question, UnityAction myYesAction, UnityAction myNoAction) {
		modalPanel.Choice (question, myYesAction, myNoAction);
	}

	public void Show (string question) {
		modalPanel.Choice (question);
	}
}