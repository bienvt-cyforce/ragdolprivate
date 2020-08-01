using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LeaderboardController : MonoBehaviour
{

	private static LeaderboardController instance;

	public static LeaderboardController Instance {
		get { 
			return instance;
		}
	}

	private const string LEADERBOARD_STARS = "your ID";
	private const string LEADERBOARD_HS = "yourdsaddas ID";
	//	private const string LEADERBOARD_Classic_Level_ID = "Endless_Level";
	//	private const string LEADERBOARD_Classic_Score_ID = "Endless_HighScore";
	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{		
		//Debug.Log (PlayerPrefs.GetInt ("TotalLevelDone"));
		StartCoroutine (checkInternetConnection ((isConnected) => {
			if (isConnected) {
				Social.localUser.Authenticate ((bool success) => {
					submitBomberStar (PlayerPrefs.GetInt ("TOTALSTAR"));
					submitBomberHighScore (PlayerPrefs.GetInt ("BESTKILL"));
				});	
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

	public void showLeaderBoardsUI ()
	{
		Social.ShowLeaderboardUI ();
		submitBomberStar (PlayerPrefs.GetInt ("TOTALSTAR"));
		submitBomberHighScore (PlayerPrefs.GetInt ("BESTKILL"));
		//Debug.Log (PlayerPrefs.GetInt("TOTALSTAR")+"LBBBBBBBBBBBBBBBBBBBB");
	}

	public void submitBomberStar (int star)
	{		
		Social.ReportScore (star, LEADERBOARD_STARS, success => {
		});
	}

	public void submitBomberHighScore (int score)
	{		
		Social.ReportScore (score, LEADERBOARD_HS, success => {
		});
	}
	//	public void submitClassicLevel(int level) {
	//		Social.ReportScore (level, LEADERBOARD_Classic_Level_ID, success => {});
	//	}
	//
	//	public void submitClassicHighScore(int score) {
	//		Social.ReportScore (score, LEADERBOARD_Classic_Score_ID, success => {});
	//	}

}
