using UnityEngine;

public class MarqueeTraffic : MonoBehaviour
{
	public string message    = "Where we're going, we don't need roads.";
	public float scrollSpeed = 50;

	public Rect messageRect;
	public GUIStyle estilo;
	Vector2 dimensions;

	void OnGUI ()
	{
		// Set up the message's rect if we haven't already
		if (messageRect.width == 0) {
			dimensions = GUI.skin.label.CalcSize(new GUIContent(message));

			// Start the message past the left side of the screen
			messageRect.x      = -dimensions.x;

		}

		dimensions = GUI.skin.label.CalcSize(new GUIContent(message));

		messageRect.width  =  dimensions.x;
		messageRect.height =  dimensions.y;

		messageRect.x -= Time.deltaTime * scrollSpeed;
		//messageRect.y = GetComponentInParent<Marquee>().messageRect.y - GetComponentInParent<Marquee>().messageRect.height - 1f;
		messageRect.y = GetComponentInParent<Marquee>().messageRect.y  + GetComponentInParent<Marquee>().messageRect.height + 2f;

		// If the message has moved past the right side, move it back to the left
		if ((messageRect.x+messageRect.width) < 0) {
			messageRect.x = Screen.width;
		}

		GUI.Label(messageRect, message, estilo);

		Rect caja = new Rect();
		caja.x = 0;
		caja.y = messageRect.y - 1f;
		caja.height = 22f;
		caja.width = Screen.width;
		GUI.Box(caja, MakeTex(1, 1, Color.black));


	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];

		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );

		result.Apply();
		return result;
	}
}
