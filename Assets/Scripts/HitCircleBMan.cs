using UnityEngine;
using System.Collections;

public class HitCircleBMan : MonoBehaviour {
	public static HitCircleBMan Instance;
	public CreateObjectPooling ob;
	void Awake(){
		Instance = this;
	}
	public void play(Vector3 pos){
		ob._ChooseEffect (pos);
	}
}
