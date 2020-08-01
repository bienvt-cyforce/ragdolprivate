using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
	public static StartManager Instance;
	public GameObject GUIStart, GUISelectMode, GUISelect, GUIContinue, GUIComingSoon, GUIExit, GUIOptions, GUIBoosters;
	public FadeScript fade;
	bool loadLevel = false;
	public bool firstTimePlayGame = false;

	public static string linkSharegame = "fb.me";

	void Awake ()
	{
		//	PlayerPrefs.DeleteAll ();
		//	PlayerPrefs.SetInt ("COIN", 1000000);

		//	PlayerPrefs.SetFloat ("GAMESPEED", 0.1f);
		Instance = this;
	//	PlayerPrefs.SetInt ("BOOSTER_KILL", 100);

		Time.timeScale = 1;
		loadLevel = (PlayerPrefs.GetInt ("ToLevel") == 1) ? true : false;
		firstTimePlayGame = (PlayerPrefs.GetInt ("FirstTimePlayGame") == 1) ? false : true;
		if (firstTimePlayGame) {
			PlayerPrefs.SetInt ("FirstTimePlayGame", 1);
			// setLevel;
			PlayerPrefs.SetInt ("Classic1", 4);
			PlayerPrefs.SetInt ("COIN", 100000);

			PlayerPrefs.SetInt ("MUSIC", 1);
			PlayerPrefs.SetInt ("SOUND", 1);
			PlayerPrefs.SetInt ("VIBRATION", 0);

			PlayerPrefs.SetInt ("BOOSTER_HEALTH", 10);
			PlayerPrefs.SetInt ("BOOSTER_STRENGH", 10);
			PlayerPrefs.SetInt ("BOOSTER_KILL", 10);

			PlayerPrefs.SetInt ("DAYLATE", System.DateTime.Now.Day);
			PlayerPrefs.SetInt ("MONTHLATE", System.DateTime.Now.Month);
			PlayerPrefs.SetInt ("DAYCOUNT", 1);


			PlayerPrefs.SetFloat ("GAMESPEED", 1f);

			BadLogic.speedVelocity = PlayerPrefs.GetFloat ("GAMESPEED");


			PlayerPrefs.SetInt ("Classicunlocked", 1);
			PlayerPrefs.Save ();
		}

		CoinManager.GetCoin ();

	}

	void Start ()
	{
		GUIBoosters.SetActive (false);
		GUIComingSoon.SetActive (false);
		GUIContinue.SetActive (false);
		GUIExit.SetActive (false);
		GUIOptions.SetActive (false);
		if (!loadLevel) {
			GUIStart.SetActive (true);
			GUISelectMode.SetActive (false);
			GUISelect.SetActive (false);
			fade.loadIn (1f);
		} else {
			string key = RAMMode.nameMode;
			LevelManager.Instance._setInfo (SelectModeManager.Instance.GetInfoMode (key));
			activeLevelManager ();
			PlayerPrefs.SetInt ("ToLevel", 0);
			PlayerPrefs.Save ();
			if (!firstTimePlayGame)
				AdsController.Instance.ShowBanner ();
		}

		MusicAndSoundMan.Instance.changeInfo ();
		MusicManager.Instance.playClipBG ();

		CoinManager.GetToltalStar ();
		//EffectFadeCamera.Instance.PlayEffect (0);
	}

	public void one_player ()
	{
		//SceneManager.LoadScene (1);
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (0);
		GUIContinue.SetActive (true);
		GUIStart.SetActive (false);
		if (!firstTimePlayGame) {
			AdsController.Instance.ShowBanner ();
		}
	
	}

	public void activeSelectMode ()
	{
		Time.timeScale = 1f;
		GUIContinue.SetActive (false);
		GUIStart.SetActive (false);
		GUISelect.SetActive (false);
		GUISelectMode.SetActive (true);
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (0);
	}

	public void activeLevelManager ()
	{
		GUIContinue.SetActive (false);
		//LevelManager.Instance._setInfo ();
		GUISelectMode.SetActive (false);
		GUIStart.SetActive (false);
		GUISelect.SetActive (true);
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
	}

	public void fade_LoadOut (float duration)
	{
		fade.loadOut (duration);
	}

	public void levelMan_clickBack ()
	{
		activeSelectMode ();
	}

	public void modeMan_back ()
	{
		GUIStart.SetActive (false);
		GUISelectMode.SetActive (false);
		one_player ();
//		fade.loadIn (0.25f);
//		EffectFadeCamera.Instance.PlayEffect (1);
	}

	public void click_Back_Continue ()
	{
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
		GUIContinue.SetActive (false);
		GUIStart.SetActive (true);
		if (!firstTimePlayGame) {
			AdsController.Instance.HideBanner ();
		}
	}

	public void click_Back_ComingSoon ()
	{
		
		GUIComingSoon.SetActive (false);
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
		GUIContinue.SetActive (false);
		GUIStart.SetActive (true);
	}

	public void click_EndlessMode ()
	{
		fade.loadIn (0.5f);
		EffectFadeCamera.Instance.PlayEffect (1);
		GUIStart.SetActive (false);
		GUIContinue.SetActive (false);
		AdsController.Instance.HideBanner ();

		StartCoroutine (loadEndlessMode ());
		//
	}

	public void click_2Player ()
	{
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
//		GUIComingSoon.SetActive (true);
		GUIStart.SetActive (false);
		GUIContinue.SetActive (false);
		StartCoroutine (load_2Player ());
	}

	public void click_Exit ()
	{
		GUIExit.SetActive (true);
		GUIComingSoon.SetActive (false);
	}

	public void click_Yes_Exit ()
	{
		Application.Quit ();
	}

	public void click_No_Exit ()
	{
		GUIExit.SetActive (false);
		GUIComingSoon.SetActive (false);
	}

	public void click_Options ()
	{
		GUIOptions.SetActive (true);
		GUIComingSoon.SetActive (false);
	}

	public void click_Back_Options ()
	{
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
		GUIOptions.SetActive (false);
		GUIStart.SetActive (true);
		GUIComingSoon.SetActive (false);
		if (!firstTimePlayGame) {
			AdsController.Instance.ShowInterstitial ();
		}
	}

	public void click_Boosters ()
	{
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (1);
		GUIBoosters.SetActive (true);
		GUIComingSoon.SetActive (false);
	}

	public void clik_Back_Boosters ()
	{
		fade.loadIn (0.25f);
		EffectFadeCamera.Instance.PlayEffect (0);
		GUIBoosters.SetActive (false);
		GUIComingSoon.SetActive (false);
	}

	public void offAll ()
	{
		GUIStart.SetActive (false);
//		GUIContinue.GetComponent<Canvas> ().enabled = false;
//		GUISelect.GetComponent<Canvas> ().enabled = false;
	}

	IEnumerator loadEndlessMode ()
	{
		if (MusicManager.Instance != null) {
			MusicManager.Instance.stopMusic ();
		}
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.25f));
//		if (!firstTimePlayGame) {
//			//GoogleMobileAdsDemoScript.Instance.ShowInterstitial ();
//		}
		SceneManager.LoadScene ("EndlessMode");
	}

	IEnumerator load_2Player ()
	{
		if (MusicManager.Instance != null) {
			MusicManager.Instance.stopMusic ();
		}
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.25f));
		//		if (!firstTimePlayGame) {
		//			//GoogleMobileAdsDemoScript.Instance.ShowInterstitial ();
		//		}
		SceneManager.LoadScene ("2 Player");
	}
}
