using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompleteMissionScript : MonoBehaviour {
	public Image checkY,checkX;
	public void changeStatus(bool status){
		checkY.gameObject.SetActive (status);
		checkX.gameObject.SetActive (!status);
	}
}
