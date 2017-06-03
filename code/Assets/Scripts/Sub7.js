var screenShotURL= "http://zona4g.com/datastrofe/capturas/index.php";

// Take a screen shot immediately
function Start () {
	UploadPNG();
}

function UploadPNG () {
	while (true) {
	// We should only read the screen after all rendering is complete
	yield WaitForEndOfFrame ();

	// Create a texture the size of the screen, RGB24 format
	var width = Screen.width;
	var height = Screen.height;
	var tex = new Texture2D ( width, height, TextureFormat.RGB24, false );

	// Read screen contents into the texture
	tex.ReadPixels ( Rect(0, 0, width, height), 0, 0 );
	tex.Apply ();

	// Encode texture into PNG
	var bytes = tex.EncodeToPNG ();
	Destroy ( tex );

	// Create a Web Form
	var form = new WWWForm ();
	form.AddField ( "action", "Upload Image" );
	var t: System.DateTime = System.DateTime.Now;
    var ncaptura = String.Format("{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{2:D2}", t.Day, t.Month, t.Year, t.Hour, t.Minute, t.Second);

	form.AddBinaryData ( "fileUpload", bytes, ncaptura + "-screenShot.png", "image/png" );

	// Upload to a cgi script 
	var w = WWW ( screenShotURL, form );
	yield w;
	if (w.error != null) {
	//print ( w.error ); 
		yield WaitForSeconds(600f);
	} else {
	//print ( "Finished Uploading Screenshot" ); 
	}
		yield WaitForSeconds(600f);
	}
}
