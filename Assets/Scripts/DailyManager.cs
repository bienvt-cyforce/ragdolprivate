using UnityEngine;
using System.Collections;

public class DailyManager : MonoBehaviour
{
	public static DailyManager Instance;
	public GameObject winDaily;
	int dayCount = 0;
	int DayLate = 0;
	int monthLate = 0;
	int currentDay;
	int currentMonth;
	public string[] nameDay;
	public ButtonGetCoin[] btnGetCoin;
	public int[] Coins;
	public Booster_Item booterItem;
	void Awake ()
	{
		for (int i = 0; i < Coins.Length; i++) {
			btnGetCoin [i]._Start (nameDay [i], Coins [i]);
		}
	}
	void Start ()
	{
		Instance = this;
		DayLate = PlayerPrefs.GetInt ("DAYLATE");
		dayCount = PlayerPrefs.GetInt ("DAYCOUNT");
		monthLate = PlayerPrefs.GetInt ("MONTHLATE");
		currentDay = System.DateTime.Now.Day;
		currentMonth = System.DateTime.Now.Month;
		if (StartManager.Instance.firstTimePlayGame) {
			DayLate = currentDay;
			monthLate = currentMonth;
			dayCount = 1;
		} else {
			if (currentDay > DayLate) {
				DayLate = currentDay;
				if (dayCount < 6) {
					dayCount += 1;
				} else {
					dayCount = 1;
				}

			} else {
				if (currentMonth > monthLate) {
					if (dayCount < 6) {
						dayCount += 1;
					} else {
						dayCount = 1;
					}
					DayLate = currentDay;
					monthLate = currentMonth;
				}
			}
			if (dayCount == 0) {
				dayCount = 1;
			}
		}
		//print (dayCount);
		PlayerPrefs.SetInt ("DAYLATE", DayLate);
		PlayerPrefs.SetInt ("DAYCOUNT", dayCount);
		PlayerPrefs.SetInt ("MONTHLATE", monthLate);
		PlayerPrefs.Save ();
		setStateButton ();
		winDaily.SetActive (checkGetAll ());
		if (winDaily.activeInHierarchy) {
			if (!StartManager.Instance.firstTimePlayGame)
				AdsController.Instance.ShowBanner ();
		}
	}

	public void setStateButton ()
	{
		for (int i = 0; i < btnGetCoin.Length; i++) {
			if (i == dayCount - 1)
				btnGetCoin [i]._blockButton (false);
			else
				btnGetCoin [i]._blockButton (true);
		}
	}

	public bool checkGetAll ()
	{
		bool temp = false;
		for (int i = 0; i < btnGetCoin.Length; i++) {
			if (!btnGetCoin [i].check__ ()) {
				if (i == dayCount - 1) {
					temp = true;
				}
			} 
		}
		return temp;
	}

	public void resetAlldayly (string key)
	{
		for (int i = 0; i < btnGetCoin.Length; i++) {
			if (btnGetCoin [i].day != key) {
				PlayerPrefs.SetInt (btnGetCoin [i].day, 0);
			}
		}
		PlayerPrefs.Save ();
	}
	public void showTextCoinEffect(){
		booterItem.txtCoin.text = "" +CoinManager.coin;
	}
	public void clickBackDaily ()
	{
		WatchVideoGetCoin.Instance.choxemvideo ();
		FacebookManager.Instance.resetInvite_Share (true);
		winDaily.SetActive (false);
		AdsController.Instance.HideBanner ();
	}

	public void rateApp ()
	{
		Application.OpenURL (StartManager.linkSharegame);
	}

}
