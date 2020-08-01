using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public static SoundManager Instance;
	public AudioSource Audio_Dame;
	public AudioSource Audio_Hit;
	public AudioClip clipHit;
	public AudioClip clipDame;
	public AudioClip clipDead_Player;
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
	void Start(){
		Instance = this;
	}
	public void playClipDame(){
		Audio_Dame.Stop ();
		Audio_Dame.PlayOneShot (clipDame);
	}
	public void playClipHit(){
		if(!Audio_Hit.isPlaying)
		Audio_Hit.PlayOneShot (clipHit);
	}
	public void playClip_Dead(){
	//	Audio_Dame.Stop ();
		Audio_Dame.PlayOneShot (clipDead_Player);
	}
}
