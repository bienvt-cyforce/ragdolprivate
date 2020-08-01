using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{
	public Text txtInfo;
	public Text txtbestKIll;
	string modeName;
	int levelName, bestkill;
	bool continueLevel = false;

	void Start ()
	{
		modeName = PlayerPrefs.GetString ("MODE_CONTINUE");
		levelName = PlayerPrefs.GetInt ("LEVEL_CONTINUE");
		bestkill = PlayerPrefs.GetInt ("BESTKILL");
		continueLevel = (modeName != "") ? true : false;
		if (continueLevel) {
			string nameUI = SelectModeManager.Instance.GetInfoMode (modeName).nameUI;
			txtInfo.text = "< " + nameUI + " " + levelName + " >";
		} else {
			txtInfo.text = "< Unavailable >";
		}
		txtbestKIll.text = "< best kill = " + bestkill + " >";
	}

	public void OnClickContinue ()
	{
		if (continueLevel) {
			StartManager.Instance.offAll ();
			StartManager.Instance.fade_LoadOut (0.5f);
			StartCoroutine (loadLevel ());
		} 
	}

	IEnumerator loadLevel ()
	{
		
		MusicManager.Instance.stopMusic ();
		RAMMode.nameMode = modeName;
		RAMMode.nameLevel = "" + levelName;
		RAMMode.replay = false;

		BadLogic.speedVelocity = PlayerPrefs.GetFloat ("GAMESPEED");

		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		SceneManager.LoadScene (modeName + "_" + levelName);
	}
}
