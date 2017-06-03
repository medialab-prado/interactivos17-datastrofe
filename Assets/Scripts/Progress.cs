using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Progress : MonoBehaviour {

	Image foregroundImage;

	public float Value
	{
		get 
		{
			if(foregroundImage != null)
				return (float)(foregroundImage.fillAmount*100);	
			else
				return 0;	
		}
		set 
		{
			if(foregroundImage != null) {
				
				if (value<=49) {
					foregroundImage.color = Color.green;
				} else if (value>49 && value<=65) {
					foregroundImage.color = Color.yellow;
				} else if (value>65 && value<=75) {
					foregroundImage.color = new Color(1f, 153/255f, 0f);
				} else if (value>75) {
					foregroundImage.color = Color.red;
				}
				foregroundImage.fillAmount = value/100f;	
			}
		} 
	}

	void Start () {
		foregroundImage = gameObject.GetComponent<Image>();		
		Value =0;
	}	




}
