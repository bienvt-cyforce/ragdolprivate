using UnityEngine;
using System.Collections;

public class CoinManager{
	public static int coin = 0;
	public static int totalStar = 0;
	public static void GetCoin(){
		coin = PlayerPrefs.GetInt ("COIN");
	}
	public static void UpCoin(){
		PlayerPrefs.SetInt ("COIN", coin);
		PlayerPrefs.Save ();
	}
	public static void GetToltalStar(){
		totalStar = PlayerPrefs.GetInt ("TOTALSTAR");
	}
	public static void UpTotalStar(){
		PlayerPrefs.SetInt ("TOTALSTAR",totalStar);
		PlayerPrefs.Save ();
	}
}
