using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public static GUIManager Instance;
	public EndGameScript endGameScript;
	public PauseGameScript pauseGameScript;
	public GameObject MissionPanel;
	public FadeScript fadeScript;
	public bool modeBoxer = false;
	[Header ("EndlessMode")]
	public bool EndlessMode = false;
	Transform cam;
	[HideInInspector]
	public bool endMode = false;
	public Button btnUnityAds;
	public bool TwoPlayerSolo = false;
	public bool joystickIsLeft = false;
	public GameObject btnJoyLeft, btnJoyRight;
	public bool NeverShowAgain = false;

	void OnEnable ()
	{
		endGameScript.gameObject.SetActive (true);
		pauseGameScript.gameObject.SetActive (true);
		MissionPanel.SetActive (true);

	}

	void Awake ()
	{
		if (fadeScript == null) {
			fadeScript = GetComponentInChildren<FadeScript> ();
		}
		
		if (Instance == null)
			Instance = this;


		transform.GetChild (0).SetParent (null);
	}

	void Start ()
	{
		if (btnJoyLeft != null) {
			joystickIsLeft = (PlayerPrefs.GetInt ("JOY_LEFT") == 0) ? false : true;
			this.setInfoJoy ();
		}
		BadLogic.win = false;
		BadLogic.lose = false;
		BadLogic.boosterStengh = false;
		//endGameScript.gameObject.SetActive (false);
		cam = Camera.main.transform;
		BadLogic.pause = true;
		//pauseGameScript.gameObject.SetActive (false);
		//MissionPanel.SetActive (true);
		StartCoroutine (delayFix ());
		if (!EndlessMode) {
			if (RAMMode.replayLevel) {
				MissionPanel.SetActive (false);
				StartCoroutine (delay_ReplayLevel ());
				RAMMode.replayLevel = false;
			} else {
				if (!TwoPlayerSolo) {
					NeverShowAgain = (PlayerPrefs.GetInt ("NEVERSHOWAGAIN") == 0) ? true : false;
					if (NeverShowAgain)
						createTutorial ();
				}
			}
		} else {
			MissionPanel.SetActive (false);
			StartCoroutine (delay_ReplayLevel ());
		}
		if (TwoPlayerSolo) {
			MissionPanel.SetActive (false);
			StartCoroutine (delay_ReplayLevel ());
		}
		endMode = false;
		// Save Data to Continue
		PlayerPrefs.SetString ("MODE_CONTINUE", RAMMode.nameMode);
		PlayerPrefs.SetInt ("LEVEL_CONTINUE", int.Parse (RAMMode.nameLevel));
		PlayerPrefs.Save ();
		if (RAMMode.nameLevel == RAMMode.maxlevel.ToString ()) {
			endMode = true;
			// done
		} else {
			endMode = false;
		}
		if (EndlessMode) {
			btnUnityAds.onClick.AddListener (delegate {
				showUnityAds ();
			});
		}
		Time.timeScale = 0;
	}

	public void createTutorial ()
	{
		GameObject go = Resources.Load<GameObject> ("canvasTutorial") as GameObject;
		Instantiate (go);
		this.MissionPanel.SetActive (false);
	}

	IEnumerator delayFix ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.25f));
		fadeScript.loadIn (0.5f);
		Time.timeScale = 0;
	}

	public void SkipMission ()
	{
		if (AdsController.Instance != null) {
			AdsController.Instance.HideBanner ();
		}
		SlowMotionScript.Instance.startCamZoom ();
		if (MusicManager.Instance != null)
			MusicManager.Instance.PlayClip ();
	}

	IEnumerator delay_ReplayLevel ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		SkipMission ();
	}

	public void offReady ()
	{
		Time.timeScale = SlowMotionScript.Instance.timeScale;
		BadLogic.pause = false;
	}

	public void OnCompleted ()
	{
		MusicManager.Instance.stopMusic ();
		BadLogic.pause = true;
		this.transform.position = new Vector3 (cam.position.x, cam.position.y, this.transform.position.z);
		if (!TwoPlayerSolo) {
			endGameScript.gameObject.SetActive (true);
			endGameScript.onWin ();
			MissionManager.Instance._setAllMisssion (true, RAMMode.nameMode, RAMMode.nameLevel);
			// check Unlock Mode
			if (endMode) {
				// done
				string nameMode = RAMMode.keyModeNext;
				PlayerPrefs.SetInt (nameMode + "1", 4);
				PlayerPrefs.SetInt (nameMode + "unlocked", 1);
				PlayerPrefs.Save ();
				CoinManager.coin += 50;
				CoinManager.UpCoin ();
				//GUIScreenSpace.Instance.setTextOnCompletedMode (nameMode);

			} else {
				//
				//GUIScreenSpace.Instance.setTextOnWin ();
			}
		} else {
			SoloModeManager.Instance._OnEndGame ("2");
		}
		SlowMotionScript.Instance.SetCamNormal ();
		Time.timeScale = 0;
		StartCoroutine (showAdmob ());
	}

	IEnumerator showAdmob ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		if (AdsController.Instance != null) {
			AdsController.Instance.ShowInterstitial ();
		}
	}

	public void OnLose ()
	{
		MusicManager.Instance.stopMusic ();
		BadLogic.pause = true;
		this.transform.position = new Vector3 (cam.position.x, cam.position.y, this.transform.position.z);
		if (!TwoPlayerSolo) {
			endGameScript.gameObject.SetActive (true);
			endGameScript._onLose (EndlessMode);
			if (!RAMMode.replay) {
				MissionManager.Instance._setAllMisssion (false, RAMMode.nameMode, RAMMode.nameLevel);
			}
		} else {
			SoloModeManager.Instance._OnEndGame ("1");
		}
		StartCoroutine (showAdmob ());

		SlowMotionScript.Instance.SetCamNormal ();
		Time.timeScale = 0;
		//GUIScreenSpace.Instance.setTextOnLose ();

	}

	public void click_ToLevels ()
	{
		MusicManager.Instance.stopMusic ();
		fadeScript.loadOut (0.5f);
		StartCoroutine (delayLoad_ToLevel ());
	}

	IEnumerator delayLoad_ToLevel ()
	{
		yield return new WaitForSecondsRealtime (0.5f);
		if (BadLogic.win) {
			if (!endMode) {
				int levelNext = int.Parse (RAMMode.nameLevel) + 1;
				if (PlayerPrefs.GetInt (RAMMode.nameMode + "" + levelNext) == 0) {
					PlayerPrefs.SetInt (RAMMode.nameMode + "" + levelNext, 4);
				}
			}
		}
		PlayerPrefs.SetInt ("ToLevel", 1);
		PlayerPrefs.Save ();
		SceneManager.LoadScene (0);
		// setInfo
	}

	public void click_Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		RAMMode.replayLevel = true;
	}

	public void click_NextLevel ()
	{
		fadeScript.loadOut (0.25f);
		StartCoroutine (delayLoadNext ());
	}

	IEnumerator delayLoadNext ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		if (!endMode) {
			int levelNext = int.Parse (RAMMode.nameLevel) + 1;
			SceneManager.LoadScene (RAMMode.nameMode + "_" + levelNext);
			RAMMode.nameLevel = "" + levelNext;
			if (PlayerPrefs.GetInt (RAMMode.nameMode + "" + levelNext) == 0) {
				PlayerPrefs.SetInt (RAMMode.nameMode + "" + levelNext, 4);
				RAMMode.replay = false;
			} else if (PlayerPrefs.GetInt (RAMMode.nameMode + "" + levelNext) != 4) {
				RAMMode.replay = true;
			} else {
				RAMMode.replay = false;
			}
		} else {
			int levelNext = 1;
			MODEINFO temp = RAMMode._MODEINFO (RAMMode.keyModeNext);

			RAMMode.nameMode = temp.keyMode;
			RAMMode.maxlevel = temp.maxLevel;
			RAMMode.keyModeNext = temp.keyNext;
			RAMMode.nameUI = temp.nameUI;
			SceneManager.LoadScene (RAMMode.nameMode + "_" + levelNext);
			RAMMode.nameLevel = "" + levelNext;
			RAMMode.replay = false;
		}
	}

	public void click_Pause ()
	{
		EnemiesManager.Instance.findeEnemies ();
		MusicManager.Instance.admusicBG.Pause ();
		BadLogic.pause = true;
		this.transform.position = new Vector3 (cam.position.x, cam.position.y, this.transform.position.z);
		GUIScreenSpace.Instance.GUIPlay.SetActive (false);
		GUIScreenSpace.Instance.btnPause.SetActive (false);
		if (!TwoPlayerSolo) {
			pauseGameScript.gameObject.SetActive (true);
		} else {
			SoloModeManager.Instance.clickPause ();
		}
		Time.timeScale = 0;
		SlowMotionScript.Instance.SetCamNormal ();
		if (AdsController.Instance != null) {
			AdsController.Instance.ShowBanner ();
		}
	}

	public void click_UnPause ()
	{
		MusicManager.Instance.admusicBG.Play ();
		BadLogic.pause = false;
		Time.timeScale = SlowMotionScript.Instance.timeScale;
		if (!TwoPlayerSolo) {
			pauseGameScript.gameObject.SetActive (false);
		}
		GUIScreenSpace.Instance.GUIPlay.SetActive (true);
		GUIScreenSpace.Instance.btnPause.SetActive (true);
		if (AdsController.Instance != null) {
			AdsController.Instance.HideBanner ();
		}
	}

	public void showUnityAds ()
	{
		if (UnityAds.Instance != null) {
			UnityAds.Instance.revivePlayer = true;
			UnityAds.Instance.ShowAd ();
			btnUnityAds.transform.parent.gameObject.SetActive (false);
		}
	}

	public void revivePlayer ()
	{
		endGameScript.gameObject.SetActive (false);
		BadLogic.lose = false;
		BadLogic.pause = false;
		MusicManager.Instance.playClipBG ();
		Time.timeScale = SlowMotionScript.Instance.timeScale;
		PlayerMovement.Instance.revivePlayer ();
	}

	public void ToMainMenu ()
	{
		MusicManager.Instance.stopMusic ();
		fadeScript.loadOut (0.15f);
		StartCoroutine (delay_loadTomenu ());
	}

	IEnumerator delay_loadTomenu ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.25f));
		SceneManager.LoadScene (0);
	}

	public void changeJoyToLeft ()
	{
		if (!joystickIsLeft) {
			joystickIsLeft = true;
			PlayerPrefs.SetInt ("JOY_LEFT", 1);
			PlayerPrefs.Save ();
			setInfoJoy ();
		}
	}

	public void changeJoyToRight ()
	{
		if (joystickIsLeft) {
			joystickIsLeft = false;
			PlayerPrefs.SetInt ("JOY_LEFT", 0);
			PlayerPrefs.Save ();
			setInfoJoy ();
		}
	}

	public void setInfoJoy ()
	{
		GUIScreenSpace.Instance.changeToLeft (!joystickIsLeft);
		btnJoyLeft.SetActive (!joystickIsLeft);
		btnJoyRight.SetActive (joystickIsLeft);
	}
}
