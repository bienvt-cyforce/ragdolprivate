using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownReadyEffect : MonoBehaviour {
	public Text txtNumber;
	public Animator anim;
	void Start(){
		txtNumber.gameObject.SetActive (false);
	}
	public void setText(string text){
		txtNumber.text = text;
		txtNumber.gameObject.SetActive (true);
		anim.Rebind ();
	}
}
