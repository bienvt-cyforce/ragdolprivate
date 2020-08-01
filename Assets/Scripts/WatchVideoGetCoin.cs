using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using Facebook.Unity.Example;

public class WatchVideoGetCoin : MonoBehaviour
{
	public static WatchVideoGetCoin Instance;
	public int countVideo = 0;
	public GameObject goPanel;
	public Text txtCoin;
	public GameObject blockClick;
	public GameObject blockQua, effetQua;
	bool watchVideo = false;
	public EventAnimBuyCompleted buy;
	public Text txtCount;
	public int videoTime;
	void Awake ()
	{
		Instance = this;
		#if UNITY_EDITOR
		string adUnitId = "1262015";
		#elif UNITY_ANDROID
		string adUnitId = "1435055";
		#elif UNITY_IPHONE
		string adUnitId = "1435056";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		watchVideo = (PlayerPrefs.GetInt ("WATCHVIDEO") == 1) ? true : false;
		countVideo = PlayerPrefs.GetInt ("COUNTVIDEO");
		blockClick.SetActive (!watchVideo);
		blockQua.SetActive (!watchVideo);
		effetQua.SetActive (watchVideo);
	}

	void Start ()
	{
		goPanel.SetActive (false);
	}

	public void choxemvideo ()
	{
		watchVideo = true;
		PlayerPrefs.SetInt ("WATCHVIDEO", 1);
		PlayerPrefs.SetInt ("COUNTVIDEO", 0);
		PlayerPrefs.Save ();
		countVideo = 0;
		blockClick.SetActive (!watchVideo);
		blockQua.SetActive (!watchVideo);
		effetQua.SetActive (watchVideo);
	}
	//	public void ShowRewardedAd()
	//	{
	//		if (SoundManager.Sound)
	//			SoundManager.Sound.Sound_ClickButton ();
	//
	//		if (Advertisement.IsReady("rewardedVideo"))
	//		{
	//			var options = new ShowOptions { resultCallback = HandleShowResult };
	//			Advertisement.Show("rewardedVideo", options);
	//		}
	//	}
	void Update ()
	{
		if (watchVideo) {
			if (Advertisement.IsReady ("")) {
				blockClick.SetActive (false);
			} else {
				blockClick.SetActive (true);	
			}
		}
	}
	//	private void HandleShowResult(ShowResult result)
	//	{
	//		switch (result)
	//		{
	//		case ShowResult.Finished:
	//			Debug.Log ("The ad was successfully shown.");
	//
	//			Defeat.instance.TakeHealthFinish ();
	//			break;
	//		case ShowResult.Skipped:
	//			Debug.Log("The ad was skipped before reaching the end.");
	//			break;
	//		case ShowResult.Failed:
	//			Debug.LogError("The ad failed to be shown.");
	//			break;
	//		}
	//	}
	public void ShowAd (string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine (WaitForAd ());
		#endif
		if (string.Equals (zone, ""))
			zone = null;
		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady (zone)) {
			Advertisement.Show (zone, options);
		}

	}

	void AdCallbackhandler (ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			//			Debug.Log ("Ad Finished. Rewarding player...");
			///	Defeat.instance.TakeHealthFinish ();
			//	GUIManager.instance.RevivePlayer ();
		//	this.randomCoin ();
			this.OnFinishVideo ();
			break;
		case ShowResult.Skipped:
			//			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			///	Defeat.instance.TakeHealthFinish ();
			//	GUIManager.instance.RevivePlayer ();
			//this.randomCoin ();
			this.OnFinishVideo ();
			break;
		case ShowResult.Failed:
			//			Debug.Log("I swear this has never happened to me before");
			//			AndroidMessage.Create("FAIL TO LOAD VIDEO"," Please check your internet connection.");
			break;
		}
	}

	IEnumerator WaitForAd ()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while (Advertisement.isShowing)
			yield return null;
		Time.timeScale = currentTimeScale;
	}

	public void OnCLickQua ()
	{
		goPanel.SetActive (true);
		txtCount.text = "" + (videoTime - countVideo);
		CoinManager.GetCoin ();
		setCoinText ();
	}

	public void clickBack ()
	{
		goPanel.SetActive (false);
	}

	public void click_ButtonGet ()
	{
		this.ShowAd ();
	}

	public void OnFinishVideo ()
	{
		PanelWatchVideo.Instance.StartAnim ();
	}

	public void randomCoin (int coin)
	{
		countVideo += 1;
		txtCount.text = "" + (videoTime - countVideo);
		CoinManager.GetCoin ();
		CoinManager.coin += coin;
		CoinManager.UpCoin ();
		setCoinText ();
		if (countVideo >= videoTime) {
			blockClick.SetActive (true);
			watchVideo = false;
			blockQua.SetActive (!watchVideo);
			effetQua.SetActive (watchVideo);
			PlayerPrefs.SetInt ("WATCHVIDEO", 0);
			PlayerPrefs.Save ();
		}
		PlayerPrefs.SetInt ("COUNTVIDEO", countVideo);
		PlayerPrefs.Save ();
		buy.setTextCoin ("+ " + coin);
	}

	public void setCoinText ()
	{
		txtCoin.text = "" + CoinManager.coin;
	}

}