using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public static MusicManager Instance;
	public AudioClip[] clipsBG;
	public AudioSource adCountTime;
	public AudioSource admusicBG;
	void OnEnable(){
		Instance = this;
	}
	public void PlayClip(){
		adCountTime.Stop ();
		adCountTime.Play ();
	}
	public void playClipBG(){
		admusicBG.Stop ();
		admusicBG.clip = clipsBG [randomIndex ()];
		admusicBG.loop = true;
		admusicBG.Play ();
	}
	public int randomIndex(){
		int i = Random.Range (1,clipsBG.Length);
		return i-1;
	}
	public void stopMusic(){
		admusicBG.Stop ();
	}
}
