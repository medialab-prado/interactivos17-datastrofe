using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazController : MonoBehaviour {

	private Color originalColor;
	private Image imagen;
	// Use this for initialization
	void Start () {
		imagen = gameObject.GetComponent<Image>();

	}
	
	void Update () {
		print(Input.GetAxis("Mover Mapa Horizontal"));
		print(Input.GetAxis("Mover Mapa Vertical"));
		if ((Input.GetAxis("Joystick Mouse X") != 0) || (Input.GetAxis("Joystick Mouse Y") != 0)) {
			if (imagen.color.a>1f) imagen.CrossFadeAlpha(1f, 0.5f, true);
		} else {
			imagen.CrossFadeAlpha(255f, 0.5f, true);
		}
			
	}
}
