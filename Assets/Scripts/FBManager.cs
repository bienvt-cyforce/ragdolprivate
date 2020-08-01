using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;

public class FBManager : MonoBehaviour {
	List<string> perms = new List<string>(){"public_profile","email","user_friends"};

	void Awake(){
		FB.Init (InitCompleteCallback, UnityCallbackDelegate);
	}
	public void Login(){		
		if (!FB.IsLoggedIn) {
			FB.LogInWithReadPermissions (perms, LoginCallback);
		} else {
			ShareLink ();
		}
	}
	public void ShareLink(){		
		FB.ShareLink (new System.Uri(StartManager.linkSharegame), null, null, null, ShareCallback);
	}
	public void Invite(){
		FB.Mobile.AppInvite (
			new System.Uri("https://fb.me/1335944783150873"),
			null, //new System.Uri("http://imageshack.com/a/img922/5166/h4OVUy.png"),
			InviteCallback
		);
	}

//	public void Challenge(){
//		FB.AppRequest (
//			"Haha!! You are studpid chicken!",
//			null,
//			new List<Object>(){"app_users"},
//			null,
//			null,
//			null,
//			null,
//			ChallengeCallback
//		);
//	}

	#region callback
	private void LoginCallback(IResult result){
		if (result.Cancelled) {
			Debug.LogError ("User Cancelled");
		} else {
			Debug.Log ("Login succesfuly");
			ShareLink ();
		}
	}
	private void ShareCallback(IShareResult result){
		if (result.Cancelled) {
			Debug.LogError ("User cancelled inviting");
		} else if (!string.IsNullOrEmpty(result.Error)) {
			Debug.LogError ("Inviting error");
		} else if(!string.IsNullOrEmpty(result.RawResult)){
			Debug.Log ("Inviting succesfuly");
			FacebookManager.Instance.AddCoinOnShare ();
		}	
	}

	private void InviteCallback(IResult result){
		if (result.Cancelled) {
			Debug.LogError ("User cancelled sharing");
			//GameController.Instance.GameOver ();
		} else if (!string.IsNullOrEmpty(result.Error)) {
			Debug.LogError ("Sharing error");
		//	GameController.Instance.GameOver ();
		} else if(!string.IsNullOrEmpty(result.RawResult)){
			//GameController.Instance.RespawnPlayer ();
			FacebookManager.Instance.AddCoinOnInvite();
		}	
	}
	private void InitCompleteCallback(){
		if (FB.IsInitialized) {
			Debug.Log ("Successfuly init");
		} else {
			Debug.Log ("Failed init");
		}
	}
	private void UnityCallbackDelegate(bool isUnity){
		if (isUnity) {
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}
	}				
	#endregion
}
