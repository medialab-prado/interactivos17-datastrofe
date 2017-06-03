using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FakeCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Joystick Mouse X")!=0 || Input.GetAxis("Joystick Mouse Y")!=0) {
			transform.position = new Vector3((transform.position.x+Input.GetAxis("Joystick Mouse X")),(transform.position.y+Input.GetAxis("Joystick Mouse Y")));
		} else
        {
            transform.position = Input.mousePosition;
        }
	}

    void OnMouseDown()
    {
        Debug.Log("MouseDown");
    }

}
