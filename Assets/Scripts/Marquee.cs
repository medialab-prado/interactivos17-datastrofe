using UnityEngine;
using UnityEngine.UI;

public class Marquee : MonoBehaviour
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
		messageRect.y = 2f; //Screen.height - GameObject.Find("CPanel").GetComponent<RectTransform>().rect.height - 22f;

		Rect caja = new Rect();
		caja.x = 0;
		caja.y = messageRect.y - 1f;
		caja.height = 22f;
		caja.width = Screen.width;
		GUI.Box(caja, "", estilo);

		// If the message has moved past the right side, move it back to the left
		if ((messageRect.x+messageRect.width) < 0) {
			messageRect.x = Screen.width;
		}

		GUI.Label(messageRect, message, estilo);
	}

}
