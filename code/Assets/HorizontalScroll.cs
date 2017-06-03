using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HorizontalScroll : MonoBehaviour
{
	public ScrollRect myScrollRect;
	public float speed;
	public float refreshdelay;

	void Update()
	{
		if (myScrollRect.horizontalNormalizedPosition < 1)
		{
			myScrollRect.horizontalNormalizedPosition = myScrollRect.horizontalNormalizedPosition + speed;
		}
		if (myScrollRect.horizontalNormalizedPosition > 1)
		{
			StartCoroutine(refresh());
		}
	}
	IEnumerator refresh()
	{
		yield return new WaitForSeconds(refreshdelay);
		myScrollRect.horizontalNormalizedPosition = 0.0f;
		StopAllCoroutines();
	}
}