using UnityEngine;
using System.Collections;

public class HiAManScript : MonoBehaviour {
	public static HiAManScript Instance;
	public CreateObjectPooling ob;
	void Awake(){
		Instance = this;
	}
	public void play(Vector3 pos){
		ob._ChooseEffect (pos);
	}
}
