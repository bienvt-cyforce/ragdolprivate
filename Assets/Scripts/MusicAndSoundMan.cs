using UnityEngine;
using System.Collections;
using System;

public class MusicAndSoundMan : MonoBehaviour {
	public static MusicAndSoundMan Instance;
	public AudioSource[] AD;
	public bool isMusic = false,isSound = false;
	void OnEnable(){
		Instance = this;
		AD = this.GetComponentsInChildren<AudioSource> ();
	}
	public void changeInfo(){
		isMusic = (PlayerPrefs.GetInt ("MUSIC") == 1) ? true : false;
		isSound = (PlayerPrefs.GetInt ("SOUND") == 1) ? true : false;
		for (int i = 0; i < AD.Length; i++) {
			if (AD [i].tag == "MUSIC") {
				AD [i].mute = !isMusic;
			} else {
				AD [i].mute = !isSound;
			}
		}
		BadLogic.isVibration =  (PlayerPrefs.GetInt ("VIBRATION") == 1) ? true : false;
	}
}
