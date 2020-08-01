using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
	public Font font;
	public int currentLevel;
	public string key;
	public Color colorStarNormal;
	public Color colorStarActive;
	public Image[] sprStar = new Image[3];
	public GameObject panelBlock;
	public Text txtInfo;
	public Button btnLevel;
	bool levelCompleted = false;

	void Awake ()
	{
		btnLevel = this.GetComponent<Button> ();
		txtInfo.font = font;
	}

	public void _LoadInfo (int LEVEL,string keyT)
	{
		key = keyT;
		currentLevel = LEVEL;
		txtInfo.text = "" + currentLevel;
		levelCompleted = false;
		int info = PlayerPrefs.GetInt (key + currentLevel);
		if (info == 0) {
			State_Stars (false);
			/// TEST MODE
//			State_Stars (true);
//			setColorStars (0);

		} else if (info == 4) {
			State_Stars (true);
			setColorStars (0);
		} else {
			levelCompleted = true;
			State_Stars (true);
			setColorStars (info);
		}
		print (this.name + "ccc@@@FASFASFASFASFASFASFAS" + info);
	}
	public void OnClickButton ()
	{
		StartManager.Instance.offAll ();
		StartManager.Instance.fade_LoadOut (0.5f);
		StartCoroutine (loadLevel ());
		AdsController.Instance.HideBanner ();
	}
	IEnumerator loadLevel ()
	{
		MusicManager.Instance.stopMusic ();
		RAMMode.nameMode = key;
		RAMMode.nameLevel = "" + currentLevel;
		RAMMode.keyModeNext = SelectModeManager.Instance.GetKeyNextMode (key);
		RAMMode.replay = levelCompleted;
		BadLogic.speedVelocity = PlayerPrefs.GetFloat ("GAMESPEED");
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		SceneManager.LoadScene (key + "_" + currentLevel);
		if (!StartManager.Instance.firstTimePlayGame) {
			//showadmob
			AdsController.Instance.ShowInterstitial ();
		}
	}
	public void State_Stars (bool state)
	{
		foreach (Image im in sprStar) {
			im.enabled = state;
		}
		panelBlock.SetActive (!state);
		btnLevel.enabled = state;
	}
	public void setColorStars (int index)
	{
		for (int i = 1; i <= 3; i++) {
			if (i <= index) {
				sprStar [i - 1].enabled = true;
			} else {
				sprStar [i - 1].enabled = false;
			}
		}
	}
}
