using UnityEngine;
using System.Collections;

public class AutoTakeTotalStar : MonoBehaviour {
	ButtonMode[] bttnMode;
	public void Start(){
		bttnMode = this.GetComponentsInChildren<ButtonMode> ();
		int total = 0;
		for (int i = 0; i < bttnMode.Length; i++) {
			total += bttnMode [i].numStarActive;
		}
		CoinManager.totalStar = total;
		CoinManager.UpTotalStar ();
//		print (CoinManager.totalStar+" @@@@@@@@@@@@@@");
	}
}
