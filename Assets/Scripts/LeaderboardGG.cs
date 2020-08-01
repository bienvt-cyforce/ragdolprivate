using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;
public class LeaderboardGG : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;

		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate ();
		StartCoroutine (checkInternetConnection ((isConnected) => {
			if (isConnected) {
				this.LogIn();
			}
		}));
	}
	IEnumerator checkInternetConnection (Action<bool> action)
	{
		WWW www = new WWW ("http://google.com");
		yield return www;
		if (www.error != null) {
			action (false);
		} else {
			action (true);
		}
	}
	// Update is called once per frame
	public void LogIn ()
	{
		print("Log in LBBBBBBBBBBBBB");
		Social.localUser.Authenticate ((bool success) =>
			{
				if (success) {
					Debug.Log ("Login Sucess");
					OnAddStar(PlayerPrefs.GetInt ("TOTALSTAR"));
					OnAdd_Kller(PlayerPrefs.GetInt ("BESTKILL"));
				} else {
					Debug.Log ("Login failed");
				}
			});
	}
	public void OnShowLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI ();
		OnAddStar(PlayerPrefs.GetInt ("TOTALSTAR"));
		OnAdd_Kller(PlayerPrefs.GetInt ("BESTKILL"));
	}
	public void OnAddStar(int star)
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (star, GPGSIds.leaderboard_hight_star, (bool success) =>
				{
					if (success) {
						Debug.Log ("Update Score Success");

					} else {
						Debug.Log ("Update Score Fail");
					}
				});
		}
	}
	public void OnAdd_Kller(int kill)
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (kill, GPGSIds.leaderboard_best_kiler, (bool success) =>
				{
					if (success) {
						Debug.Log ("Update Score Success");

					} else {
						Debug.Log ("Update Score Fail");
					}
				});
		}
	}
	public void OnLogOut ()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
	}
}
