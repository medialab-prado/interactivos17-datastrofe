using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Camera Controller Isometric
// Allows the camera to move left, right along a fixed axis, zoom in and out.
// Attach to a camera GameObject (e.g IsoCamera) for functionality.

public class CameraController : MonoBehaviour {

	//Include the CameraObject
	public Camera IsoCamera;
	public int cameraVelocity = 10;
	public float cameraZoomStep = 2f;
	float cameraRotationStep = 90f;
	float cameraRotationQueued = 0f;
	Quaternion newRotation;
	public string cameraPointing = "N";
	public float mouseAxisCorrection = 10.5f;
	public GameObject interfaz;
	public GameObject listener;

	public float cameraMaxSize = 300f;
	public float cameraMinSize = 50f;
	private float t;


	void Awake() {

	}

	void Start () {
	}

	void Update ()
	{
		KeyboardControl(); // call Keyboard controls
		//MouseControl(); // call Mouse controls
		GamePadControl(); // call GamePad controls
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.2f);
		//listener.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	void RotateRight()
	{
		//Keeping track of camera orientation to translate correctly
		if (cameraPointing == "N"){
			cameraPointing = "E";
		} else if (cameraPointing == "E"){
			cameraPointing = "S";
		} else if (cameraPointing == "S"){
			cameraPointing = "W";
		} else if (cameraPointing == "W"){
			cameraPointing = "N";
		}
		cameraRotationQueued -= cameraRotationStep; //adding a left step to rotation needed to be done
		newRotation = Quaternion.AngleAxis(cameraRotationQueued, Vector3.up);
	}

	void RotateLeft()
	{
		//Keeping track of camera orientation to translate correctly
		if (cameraPointing == "N"){
			cameraPointing = "W";
		} else if (cameraPointing == "W"){
			cameraPointing = "S";
		} else if (cameraPointing == "S"){
			cameraPointing = "E";
		} else if (cameraPointing == "E"){
			cameraPointing = "N";
		}
		cameraRotationQueued += cameraRotationStep; //adding a right step to ritation needed to be done
		newRotation = Quaternion.AngleAxis(cameraRotationQueued, Vector3.up);
	}

	void TranslateLeft(){


		switch (cameraPointing) {
		case "N":
			transform.Translate((Vector3.left * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.up * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "W":
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.down * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "E":
			transform.Translate((Vector3.left * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.down * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "S":
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.up * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		default:
			break;
		}
	}

	void TranslateRight(){
		switch (cameraPointing) {
		case "N":
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.down * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "W":
			transform.Translate((Vector3.left * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.up * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "E":
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.up * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		case "S":
			transform.Translate((Vector3.left * cameraVelocity) * Time.deltaTime, Space.World);
			transform.Translate((Vector3.down * 0.4f * cameraVelocity) * Time.deltaTime, Space.World);
			break;
		default:
			break;
		}
	}

	void TranslateUp(){
		transform.Translate((Vector3.up * cameraVelocity) * Time.deltaTime, Space.World);
	}
	void TranslateDown(){
		transform.Translate((Vector3.down * cameraVelocity) * Time.deltaTime, Space.World);
	}

	// Keyboard input controls
	void KeyboardControl(){
		// Left (screen-wise)
		if((Input.GetKey(KeyCode.LeftArrow)))
		{
			TranslateLeft();
		}
		// Right (screen-wise)
		if((Input.GetKey(KeyCode.RightArrow)))
		{
			TranslateRight();
		}
		// Up
		if((Input.GetKey(KeyCode.UpArrow)))
		{
			TranslateUp();
		}
		// Down
		if(Input.GetKey(KeyCode.DownArrow))
		{
			TranslateDown();
		}
		// rotate left one step when key pressed
		if (Input.GetKeyDown(KeyCode.A))
		{
			RotateLeft ();
		}
		//rotate right one step when key pressed
		if (Input.GetKeyDown(KeyCode.E))
		{
			RotateRight ();
		}
	}

	//GamePad input controls
	void GamePadControl() {

	

		t += Time.deltaTime;
		if (Input.GetAxis("Menos Zoom")>0){
			if (IsoCamera.orthographicSize<=cameraMinSize) return;
			//IsoCamera.orthographicSize -= Input.GetAxis("LTrigger")*25f*Time.deltaTime;
			IsoCamera.orthographicSize = Mathf.SmoothStep(IsoCamera.orthographicSize, IsoCamera.orthographicSize-20f, t);
		} 

		if (Input.GetAxis("Más Zoom")>0) {
			if (IsoCamera.orthographicSize>=cameraMaxSize) return;
			//IsoCamera.orthographicSize += Input.GetAxis("RTrigger")*25f*Time.deltaTime;
			IsoCamera.orthographicSize = Mathf.SmoothStep(IsoCamera.orthographicSize, IsoCamera.orthographicSize+20f, t);

		}

		if (Input.GetButtonDown("Girar mapa izquierda")) RotateLeft();
		if (Input.GetButtonDown("Girar mapa derecha")) RotateRight();

		float hValue = Input.GetAxis ("Mover Mapa Horizontal");
		float hValueAbs = Mathf.Abs (hValue)*mouseAxisCorrection;

		if (hValue>0) {
			switch (cameraPointing) {
			case "N":
				transform.Translate((Vector3.right * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.down * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "W":
				transform.Translate((Vector3.left * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.up * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "E":
				transform.Translate((Vector3.right * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.up * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "S":
				transform.Translate((Vector3.left * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.down * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			default:
				break;
			}
			interfaz.GetComponent<Image>().CrossFadeAlpha(0.01f, 0.2f, false);

		}
		if (hValue<0) {
			switch (cameraPointing) {
			case "N":
				transform.Translate((Vector3.left * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.up * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "W":
				transform.Translate((Vector3.right * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.down * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "E":
				transform.Translate((Vector3.left * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.down * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			case "S":
				transform.Translate((Vector3.right * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				transform.Translate((Vector3.up * 0.4f * cameraVelocity * hValueAbs) * Time.deltaTime, Space.World);
				break;
			default:
				break;
			}
			interfaz.GetComponent<Image>().CrossFadeAlpha(0.01f, 0.2f, false);

		} 

		if (Input.GetAxis ("Mover Mapa Vertical")>0){
			transform.Translate((Vector3.up * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mover Mapa Vertical"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
			interfaz.GetComponent<Image>().CrossFadeAlpha(0.01f, 0.2f, false);
		}
		if (Input.GetAxis ("Mover Mapa Vertical")<0){
			transform.Translate((Vector3.down * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mover Mapa Vertical"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
			interfaz.GetComponent<Image>().CrossFadeAlpha(0.01f, 0.2f, false);
		}

		interfaz.GetComponent<Image>().CrossFadeAlpha(1f, 0.4f, false);

	
		
	}

	//Mouse input controls
	void MouseControl(){
		//Zooming
		//First, adjust Zoom Step depending on current Zoom level
		if (IsoCamera.orthographicSize < 8.5f){
			cameraZoomStep = 1f;
		} else if (IsoCamera.orthographicSize < 20f){
			cameraZoomStep = 5f;
		} else if (IsoCamera.orthographicSize < 50f){
			cameraZoomStep = 10f;
		} else {
			cameraZoomStep = 20f;
		}
		//Zoom when mouse wheel used
		if (!Input.GetMouseButton(0)){ // Make sure the player isn't trying to rotate rather than zoom
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				IsoCamera.orthographicSize = Mathf.Clamp(IsoCamera.orthographicSize+cameraZoomStep, 1f, 50f);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				IsoCamera.orthographicSize = Mathf.Clamp(IsoCamera.orthographicSize-cameraZoomStep, 1f, 50f);
			}
		}

		//Panning and rotating with mouse via left button
		if (Input.GetMouseButton(0)){
			if (Input.GetAxis ("Mouse X")<0) {
				if(cameraPointing == "N"){
					transform.Translate((Vector3.right * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.down * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else if (cameraPointing == "E"){
					transform.Translate((Vector3.right * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.up * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else if (cameraPointing == "S") {
					transform.Translate((Vector3.left * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.down * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else {
					transform.Translate((Vector3.left * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.up * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				}
			}
			if (Input.GetAxis ("Mouse X")>0) {
				if(cameraPointing == "N"){
					transform.Translate((Vector3.left * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.up * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else if (cameraPointing == "E"){
					transform.Translate((Vector3.left * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.down * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else if (cameraPointing == "S"){
					transform.Translate((Vector3.right * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.up * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				} else if (cameraPointing == "W"){
					transform.Translate((Vector3.right * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
					transform.Translate((Vector3.down * 0.4f * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse X"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
				}
			}
			if (Input.GetAxis ("Mouse Y")<0){
				transform.Translate((Vector3.up * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse Y"))*mouseAxisCorrection) * Time.deltaTime, Space.Self);
			}
			if (Input.GetAxis ("Mouse Y")>0){
				transform.Translate((Vector3.down * cameraVelocity * Mathf.Abs (Input.GetAxis ("Mouse Y"))*mouseAxisCorrection) * Time.deltaTime, Space.World);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				RotateLeft ();
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				RotateRight ();
			}
		}
	}
}

