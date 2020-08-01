using UnityEngine;
using System.Collections;

public class OldTVScript : MonoBehaviour {
	public TweenAlpha twAl;
	public void PlayOldTV(){
		twAl.enabled = true;
		twAl.ResetToBeginning ();
		twAl.Play ();
	}
	public void OffOldTV(){
		this.gameObject.SetActive (false);
	}
}
