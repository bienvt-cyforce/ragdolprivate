using UnityEngine;
using System.Collections;

public class EffectFadeCamera : MonoBehaviour {
	public static EffectFadeCamera Instance;
	Camera camMain;
	public Vector3 From;
	public Vector3[] To = new Vector3[2];
	public float duration;
	bool playEffect = false;
	Vector3 target;
	void Start(){
		camMain = Camera.main;
		Instance = this;
	}
	void Update(){
		if (playEffect) {
			camMain.transform.position = Vector3.MoveTowards (camMain.transform.position, target, duration);
			if (Vector3.Distance (camMain.transform.position, target) <= 0.1f) {
				playEffect = false;
				camMain.transform.position = From;
			}
		}
	}
	public void PlayEffect(int index){
		playEffect = true;
		target = To [index];
	}
}
