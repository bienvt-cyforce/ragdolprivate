using UnityEngine;
using System.Collections;

public class PanelMission : MonoBehaviour {
	public void clickStart(){
		GUIManager.Instance.SkipMission ();
		this.gameObject.SetActive (false);
	}
}
