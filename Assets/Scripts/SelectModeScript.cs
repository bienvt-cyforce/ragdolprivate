using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectModeScript : MonoBehaviour {
	public static SelectModeScript Instance;
	public Text txtCoin;
	void OnEnable(){
		if (Instance == null)
			Instance = this;
	}
	void Start(){
		CoinManager.GetCoin ();
		setCoin (CoinManager.coin);
	}
	public void setCoin(int coin){
		txtCoin.text = "" + coin;
	}
}
