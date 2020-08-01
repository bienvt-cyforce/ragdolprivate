using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonGetCoinInApp : MonoBehaviour
{
	public int CoinGet;
	public float price;
	public Text txtCoin;
	public Text txtPrice;
	public Button btnGetCoin;
	InAppManager inAppMan;
	int index;
	void Start ()
	{
		btnGetCoin.onClick.AddListener (delegate {
			_OnClick ();
		});
	}

	public void Setup (int coin, float priceTemp,InAppManager temp,int i)
	{
		this.index = i;
		this.inAppMan = temp;
		this.CoinGet = coin;
		this.price = priceTemp;
		txtCoin.text = "" + CoinGet;
		txtPrice.text = "" + price+"$";
	}
	public void _OnClick ()
	{
		this.inAppMan.ClickGetCoin (index);	
	}
}
