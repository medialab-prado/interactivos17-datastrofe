using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class ModalPanel : MonoBehaviour {

	public Text question;
	public Image iconImage;
	public Button yesButton;
	public Button noButton;
	public Button cancelButton;
	public GameObject modalPanelObject;
	public float fadeTime;
	public float displayTime;

	private IEnumerator fadeAlpha;
	//private static ModalPanel modalPanel;

	/*
	public static ModalPanel Instance () {
		
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalPanel;
	}*/
		
	void Update() {
		if (Input.GetButton("Yes")) {
			print("YES");
			yesButton.onClick.Invoke();
			yesButton.onClick.RemoveAllListeners();
			ClosePanel();
		} else if (Input.GetButton("No")) {
			print("NO");
			noButton.onClick.Invoke();
			noButton.onClick.RemoveAllListeners();
			ClosePanel();
		}  else if (Input.GetButton("No")) {
			print("CANCEL");
			cancelButton.onClick.Invoke();
			cancelButton.onClick.RemoveAllListeners();
			ClosePanel();
		}
	}

	public void Choice (string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {
		modalPanelObject.SetActive (true);

		yesButton.onClick.RemoveAllListeners();
		yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (ClosePanel);

		noButton.onClick.RemoveAllListeners();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (ClosePanel);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

		this.question.text = question;

		this.iconImage.gameObject.SetActive (false);
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (true);
	}

	public void Choice (string question, UnityAction yesEvent, UnityAction noEvent) {
		modalPanelObject.SetActive (true);

		yesButton.onClick.RemoveAllListeners();
		yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (ClosePanel);

		noButton.onClick.RemoveAllListeners();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (ClosePanel);

		this.question.text = question;

		this.iconImage.gameObject.SetActive (false);
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (false);
	}

	public void Choice (string question) {
		modalPanelObject.SetActive (true);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (ClosePanel);

		this.question.text = question;

		this.iconImage.gameObject.SetActive (false);
		yesButton.gameObject.SetActive (false);
		noButton.gameObject.SetActive (false);
		cancelButton.gameObject.SetActive (true);
	}

	void ClosePanel () {
		SetAlpha();
		modalPanelObject.SetActive (false);
	}

	IEnumerator SetAlpha () {
		if (fadeAlpha != null) {
			StopCoroutine (fadeAlpha);
		}
		yield return FadeAlpha();
	}


	IEnumerator FadeAlpha () {
		Color resetColor = modalPanelObject.GetComponent<Image>().color;
		resetColor.a = 1;
		modalPanelObject.GetComponent<Image>().color = resetColor;

		yield return new WaitForSeconds (displayTime);

		while (modalPanelObject.GetComponent<Image>().color.a > 0) {
			Color modalPanelColor = modalPanelObject.GetComponent<Image>().color;
			modalPanelColor.a -= Time.deltaTime / fadeTime;
			modalPanelObject.GetComponent<Image>().color = modalPanelColor;
			yield return null;
		}
		yield return null;
	}


}