using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionChil : MonoBehaviour {
	public string key;
	public Button[] bttn;
	public GameObject go_On,go_Off;
	bool _status;
	void Start(){
		_status = (PlayerPrefs.GetInt (key) == 1) ? true : false;
		changeStatus (_status);
		for (int i = 0; i < bttn.Length; i++) {
			bttn [i].onClick.AddListener (delegate {
				OnClickButton ();
			});
		}
	}
	public void changeStatus(bool status){
		go_On.SetActive (status);
		go_Off.SetActive (!status);
	}
	public void OnClickButton(){
		_status = !_status;
		changeStatus (_status);
		UpToPref ();
	}
	public void UpToPref(){
		int value = (_status) ? 1 : 0;
		PlayerPrefs.SetInt (key, value);
		PlayerPrefs.Save ();
		MusicAndSoundMan.Instance.changeInfo ();
	}
}
