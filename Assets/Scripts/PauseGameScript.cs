using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour {
	public static PauseGameScript Instance;
	public GameObject panelBlockModeEndless;
	public GameObject[] blockItem;
	bool state;
	void Awake(){
		Instance = this;
	}
	void Start(){
		state = (GameObject.FindObjectOfType<GUIManager>().EndlessMode)? true:false;
		panelBlockModeEndless.SetActive (state);
		if (state) {
			for (int i = 0; i < 3; i++) {
				blockItem [i].SetActive (!state);
			}
		}
		this.gameObject.SetActive (false);
	}
	public void setStateBlock(bool a,bool b,bool c){
		if (!state) {
			blockItem [0].SetActive (a);
			blockItem [1].SetActive (b);
			blockItem [2].SetActive (c);
		}
	}
}
