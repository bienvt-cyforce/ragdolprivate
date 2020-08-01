using UnityEngine;
using System.Collections;

public class FacebookManager : MonoBehaviour
{
	public static FacebookManager Instance;
	public EventAnimBuyCompleted buyComplete;
	public GameObject faceWindow;
	public static bool invite, share;
	public GameObject blockShare,blockInvite;
	void Awake ()
	{
		//		invite = false;
		//		share = false;
		Instance = this;
		invite = (PlayerPrefs.GetInt ("FB_INVITE") == 1) ? true : false;
		share = (PlayerPrefs.GetInt ("FB_SHARE") == 1) ? true : false;
		blockInvite.SetActive (!invite);
		blockShare.SetActive (!share);
	}

	void Start ()
	{
		faceWindow.SetActive (false);
	}

	public void activeFaceWindow (bool state)
	{
		faceWindow.SetActive (state);
	}

	public void resetInvite_Share (bool state)
	{
		blockInvite.SetActive (!state);
		blockShare.SetActive (!state);
		invite = state;
		share = state;
		if (state) {
			PlayerPrefs.SetInt ("FB_INVITE", 1);
			PlayerPrefs.SetInt ("FB_SHARE", 1);
			PlayerPrefs.Save ();
		}
	}

	public void AddCoinOnShare ()
	{
		if (share) {
			CoinManager.GetCoin ();
			CoinManager.coin += 50;
			buyComplete.setTextCoin ("+50");
			CoinManager.UpCoin ();
			share = false;
			blockShare.SetActive (true);
			PlayerPrefs.SetInt ("FB_SHARE", 0);
			PlayerPrefs.Save ();
		} 
	}

	public void AddCoinOnInvite ()
	{
		if (invite) {
			CoinManager.GetCoin ();
			CoinManager.coin += 50;
			buyComplete.setTextCoin ("+50");
			CoinManager.UpCoin ();
			invite = false;
			blockInvite.SetActive (true);
			PlayerPrefs.SetInt ("FB_INVITE", 0);
			PlayerPrefs.Save ();
		} 
	}
}
