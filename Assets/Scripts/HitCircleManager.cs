using UnityEngine;
using System.Collections;

public class HitCircleManager : MonoBehaviour {
	public CreateObjectPooling ob;
	public static HitCircleManager Instance;
	void Start(){
		Instance = this;
	}
	public void playHit(Vector3 pos){
		ob._ChooseEffect (pos);
	}
}
