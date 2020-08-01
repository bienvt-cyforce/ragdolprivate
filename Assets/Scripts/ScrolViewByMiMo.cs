using UnityEngine;
using System.Collections;
using System;


public class ScrolViewByMiMo : MonoBehaviour
{
	public RectTransform rectContainer;
	bool dragging = false;
	public float maxY;
	public float minY;
	public float[] distance;
	void Update ()
	{
		if (Math.Abs (rectContainer.anchoredPosition.y - distance [0]) > 240f) {
			if (rectContainer.anchoredPosition.y < minY) {
				LerpToBttn (minY + 1f);
			}
			else if (rectContainer.anchoredPosition.y > maxY) {
				LerpToBttn (maxY - 1f);
			}
		} else {
			if (!dragging) {
				LerpToBttn (distance [0]);
			}
		}
	}
	void LerpToBttn (float position)
	{
		float newY = Mathf.Lerp (rectContainer.anchoredPosition.y, position, Time.deltaTime*20f);
		Vector2 newPosition = new Vector2 (rectContainer.anchoredPosition.x, newY);
		rectContainer.anchoredPosition = newPosition;
	}

	public void StartDrag ()
	{
		dragging = true;
	}

	public void UnDrag ()
	{
		dragging = false;
	}
}
