using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoosterScript : MonoBehaviour {
	public BoosterManager boosterMan;
	public Button btn;
	public Text txtInfo;
	public int index;
	int Value;
	bool state;

	void Awake(){
		btn.onClick.AddListener (delegate {
			OnClickBtn ();
		});
	}
	public void setInfo(int value){
		txtInfo.text = "" + value;
		Value = value;
		checkEnable ();
	}
	void checkEnable(){
		state = (Value > 0) ? true : false;
		btn.enabled = state;
	}
	public void OnClickBtn(){
		boosterMan.ActiveBooster (index);
	}
}
