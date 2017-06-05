using UnityEngine; 
using System.Collections; 
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour  
{ 
	//The texture that's going to replace the current cursor 
	public Texture2D cursorTexture; 
	public Texture2D cursorTexturePressed; 
	public Text cursorInfo;

	//This variable flags whether the custom cursor is active or not 
	public bool ccEnabled = false; 

	void Start() 
	{ 
		//Call the 'SetCustomCursor' (see below) with a delay of 2 seconds.  
		Invoke("SetCustomCursor",2.0f); 
	} 

	void OnDisable()  
	{ 
		//Resets the cursor to the default 
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
		//Set the _ccEnabled variable to false 
		this.ccEnabled = false; 
	} 

	private void SetCustomCursor() 
	{ 
		Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto); 
		this.ccEnabled = true; 
	} 

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Cursor.SetCursor(this.cursorTexturePressed, Vector2.zero, CursorMode.Auto); 	
		}
		if (Input.GetMouseButtonUp(0)) {
			Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto); 	
		}

		updateCursorInfo();
	}


	private void updateCursorInfo() {
		//cursorInfo.transform.position = Input.mousePosition;
	}
} 