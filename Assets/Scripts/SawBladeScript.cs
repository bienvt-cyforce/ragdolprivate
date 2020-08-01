using UnityEngine;
using System.Collections;

public class SawBladeScript : MonoBehaviour {
	public AudioSource ads;
	void Start(){
		ads.mute = (PlayerPrefs.GetInt("SOUND") == 1)? false: true;
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (ads != null) {
			if (coll != null) {
				if (!ads.isPlaying) {
					ads.PlayOneShot (ads.clip);
				}
			}
		}
	}
}
