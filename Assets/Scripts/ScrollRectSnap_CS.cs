using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap_CS : MonoBehaviour {
	public RectTransform panel; // To Hold the ScrollPanel
	public Button[] bttn;		//
	public RectTransform center; // Center to compare the distance for each button


	//
	private float[] distance;  // all button' distance to the center
	private bool dragging = false;
	private int bttnDistance; // will hild the distance between the button
	private int  minButtonNum; // To hold the number of the button. with

	void Start(){
		int bttnLenght = bttn.Length;
		distance = new float[bttnLenght];

		bttnDistance = (int)Mathf.Abs (bttn [1].GetComponent<RectTransform> ().anchoredPosition.x - bttn [0].GetComponent<RectTransform> ().anchoredPosition.x);
		//bttnDistance /= 100;
		print (bttnDistance);
	}
	void Update(){
		for (int i = 0; i < bttn.Length; i++) {
			distance [i] = Mathf.Abs (center.transform.position.x - bttn[i].transform.position.x);
			distance [i] = distance [i] * 100f;
		}
		float minDistance = Mathf.Min (distance);

		for (int a = 0; a < bttn.Length; a++) {
			if (minDistance == distance [a]) {
				minButtonNum = a;
			}
		}
		if (!dragging) {
			LerpToBttn (minButtonNum * -bttnDistance);
		}
	}
	void LerpToBttn(int position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}
	public void StartDrag(){
		dragging = true;
	}
	public void EndDrag(){
		dragging = false;
	}
}
