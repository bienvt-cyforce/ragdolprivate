using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TitleSkillEffect : MonoBehaviour {
	public Text txtTitle;
	public Animator anim;
	public void SetText(string text){
		txtTitle.text = text;
		StartCoroutine (delay ());
	}
	IEnumerator delay(){
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);
		anim.Rebind ();
	}
}
