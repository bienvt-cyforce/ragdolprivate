using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
	public GameObject titleLose, titleWin, btnNextLevel;
	public GameObject endgameNormal, endLessModeGo;
	public Text txtBestkill, txtCurrentkill;
	void Start(){
		this.gameObject.SetActive (false);
	}
	public void onWin ()
	{
		GUIScreenSpace.Instance.GUIPlay.SetActive (false);
		GUIScreenSpace.Instance.btnPause.SetActive (false);
		titleWin.SetActive (true);
		titleLose.SetActive (false);
		btnNextLevel.SetActive (true);
		endgameNormal.SetActive (true);
		endLessModeGo.SetActive (false);
	}

	public void _onLose (bool endlessMode)
	{
		GUIScreenSpace.Instance.GUIPlay.SetActive (false);
		GUIScreenSpace.Instance.btnPause.SetActive (false);
		if (!endlessMode) {
			endgameNormal.SetActive (true);
			endLessModeGo.SetActive (false);
			titleLose.SetActive (true);
			titleWin.SetActive (false);
			btnNextLevel.SetActive (false);
		} else {
			endgameNormal.SetActive (false);
			endLessModeGo.SetActive (true);
			getBestKill ();
		}
	}

	public void getBestKill ()
	{
		int currentKill = EndlessModeManager.Instance_.count;
		int best = PlayerPrefs.GetInt ("BESTKILL");
		if (currentKill > best) {
			best = currentKill;
			PlayerPrefs.SetInt ("BESTKILL", best);
			PlayerPrefs.Save ();
		}
		txtBestkill.text = "" + best;
		txtCurrentkill.text = "" + currentKill;
	}
}
