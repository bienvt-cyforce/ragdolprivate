using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Booster : MonoBehaviour {
	public Text txtValue;
	public string key_Booster;
	public string keyUI;
	public int price;
	int value = 0;
	public Button btnBuy;
	Booster_Item booster_item;
	void Start(){
	//	btnBuy = this.GetComponent<Button> ();
		btnBuy.onClick.AddListener (delegate {
			OnClickBuy();	
		});
		booster_item = this.GetComponentInParent<Booster_Item> ();
	}
	public void OnEnable(){
		value = PlayerPrefs.GetInt (key_Booster);
		setValueText (value);
	}
	public void setValueText(int val){
		txtValue.text = "X " + val;
	}
	public bool BuyCompleted(int coin){
		if (coin >= price) {
			return true;
		} else {
			return false;
		}
	}
	public void OnClickBuy(){
		if (BuyCompleted (CoinManager.coin)) {
			CoinManager.coin -= price;
			CoinManager.UpCoin ();
			booster_item.txtCoin.text = "" + CoinManager.coin;
			booster_item.eventbuy.setText (keyUI);
			UpdateBooster ();
		} else {
			print ("Khong du tien");
		}
	}
	public void UpdateBooster(){
		value += 1;
		PlayerPrefs.SetInt (key_Booster, value);
		PlayerPrefs.Save ();
		setValueText (value);
	}
}
