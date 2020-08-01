using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour
{
	private static UnityAds instance;

	public static UnityAds Instance {
		get { 
			return instance;
		}
	}

	public bool revivePlayer = false;

	void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void Awake ()
	{
		instance = this;
		#if UNITY_EDITOR
		string adUnitId = "1262015";
		#elif UNITY_ANDROID
		string adUnitId = "1435055";
		#elif UNITY_IPHONE
		string adUnitId = "1435056";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		Advertisement.Initialize (adUnitId, true);
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

		if (Advertisement.IsReady (zone))
			Advertisement.Show (zone, options);
	}

	void AdCallbackhandler (ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			//			Debug.Log ("Ad Finished. Rewarding player...");
			if (revivePlayer) {
				GUIManager.Instance.revivePlayer ();
			}
		///	Defeat.instance.TakeHealthFinish ();
		//	GUIManager.instance.RevivePlayer ();
			revivePlayer = false;
			break;
		case ShowResult.Skipped:
			//			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
		///	Defeat.instance.TakeHealthFinish ();
		//	GUIManager.instance.RevivePlayer ();
			if (revivePlayer) {
				GUIManager.Instance.revivePlayer ();
			}
			revivePlayer = false;
			break;
		case ShowResult.Failed:
			//			Debug.Log("I swear this has never happened to me before");
//			AndroidMessage.Create("FAIL TO LOAD VIDEO"," Please check your internet connection.");
			revivePlayer = false;
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
}