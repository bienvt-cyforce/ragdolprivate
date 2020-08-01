using UnityEngine;
using System.Collections;

public class HitBMan : MonoBehaviour {

	public static HitBMan Instance;
	public CreateObjectPooling ob;
	void Awake(){
		Instance = this;

	}
	public void play(Vector3 pos){
		ob._ChooseEffect (pos);
	}
}
