using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Booster_Item : MonoBehaviour {
	public Text txtCoin;
	public EventAnimBuyCompleted eventbuy;
	public void OnEnable(){
		CoinManager.GetCoin ();
		txtCoin.text =""+ CoinManager.coin;
	}
}
