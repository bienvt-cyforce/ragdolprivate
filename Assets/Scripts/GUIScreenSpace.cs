using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScreenSpace : MonoBehaviour
{
	
	public bool twoPlayer = false;
	public static GUIScreenSpace Instance;
	public Text txtTime;
	public Text txtCoin;
	float timer = 0;
	public CountdownReadyEffect countdownready, panelWin_Lose;
	public GameObject GUIPlay, btnPause, CanvasPlayerSecond;
	bool endLessMode = false;
	Text txtTitleTime;
	public GameObject canvasJoyLeft;
	public GameObject canvasJoyRight;
	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
		timer = 0;
		setCoin (CoinManager.coin);
		GUIPlay.SetActive (false);
		twoPlayer = (CanvasPlayerSecond != null) ? true : false;
		btnPause.SetActive (false);
	}

	void Start ()
	{
		endLessMode = GUIManager.Instance.EndlessMode;
		if (endLessMode) {
			txtCoin.gameObject.SetActive (false);
			txtTitleTime = this.transform.FindChild ("GUIPlay").transform.FindChild ("txtTitle").GetComponent<Text> ();
			txtTitleTime.text = "kill :";
		}
		if (twoPlayer) {
			CanvasPlayerSecond.SetActive (false);
		}
	}

	public void setTextOnWin ()
	{
		setTextOnWin_Lose ("WIN!");
	}

	public void setTextOnLose ()
	{
		setTextOnWin_Lose ("LOSE!");
	}

	public void setTextSoloMode (string text)
	{
		setTextOnWin_Lose ("Player " + text + " win !");
	}
	public void setTextOnCompletedMode (string nameMode)
	{
		setTextOnWin_Lose (nameMode);
	}
	public void setTextOnWin_Lose (string text)
	{
		panelWin_Lose.setText (text);
	}

	public void clickPause ()
	{
		GUIManager.Instance.click_Pause ();
	}

	public void setTextTime (int time)
	{
		txtTime.text = " " + time;
	}

	void Update ()
	{
		if (!endLessMode) {
			if (!BadLogic.win && !BadLogic.lose && !BadLogic.pause) {
				timer += Time.deltaTime;
				setTextTime ((int)timer);
			}
		}
	}

	public bool checkCompleteInSeconds (int value)
	{
		if (timer <= value) {
			return true;
		} else {
			return false;
		}
	}

	public void setCoin (int text)
	{
		txtCoin.text = "" + text;
	}

	public void startCountReady ()
	{
		countdownready.gameObject.SetActive (true);
		GUIPlay.SetActive (true);
		if (twoPlayer) {
			CanvasPlayerSecond.SetActive (true);
		}
		StartCoroutine (delayCountdown ());
	}

	IEnumerator delayCountdown ()
	{
		countdownready.setText ("3");
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
		countdownready.setText ("2");
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
		countdownready.setText ("1");
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
		countdownready.setText ("GO!");
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
		if (MusicManager.Instance != null)
			MusicManager.Instance.playClipBG ();
		// Start Game
		SlowMotionScript.Instance.camstart = false;
		countdownready.txtNumber.gameObject.SetActive (false);
		countdownready.gameObject.SetActive (false);
		GUIManager.Instance.offReady ();
		btnPause.SetActive (true);
	}
	public void changeToLeft(bool state){
		if (canvasJoyLeft) {
			canvasJoyLeft.SetActive (state);
			canvasJoyRight.SetActive (!state);
		}
	}
}
