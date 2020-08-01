using UnityEngine;
using System.Collections;

public class EventAnimBasic : MonoBehaviour {
	public void disableGo(){
		this.gameObject.SetActive (false);
	}
	public void disableGoParent(){
		this.transform.parent.gameObject.SetActive (false);
	}
}
