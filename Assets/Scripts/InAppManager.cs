using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InAppManager : MonoBehaviour
{
	public static InAppManager Instance;
	//	public Purchaser purchaser;
	public ButtonGetCoinInApp[] btns;
	public int[] coinsGet;
	public float[] prices;
	public GameObject pannelInApp;
	//	public PanelAnimator panelAnim;
	//public bool isInGame = false;
	public EventAnimBuyCompleted effectAddCoin;
	public Text txtCoin;
	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
		for (int i = 0; i < btns.Length; i++) {
			btns [i].Setup (coinsGet [i], prices [i], this, i);
		}
	}

	public void ClickGetCoin (int index)
	{
		switch (index) {
		case 0:
			Purchaser.Instance.BUYPRODUCT1 ();
			break;
		case 1:
			Purchaser.Instance.BUYPRODUCT2 ();
			break;
		case 2:
			Purchaser.Instance.BUYPRODUCT3 ();
			break;
		case 3:
			Purchaser.Instance.BUYPRODUCT4 ();
			break;
		case 4:
			Purchaser.Instance.BUYPRODUCT5 ();
			break;
		case 5:
			Purchaser.Instance.BUYPRODUCT6 ();
			break;
		}
	}
	public void AddCoin (int index)
	{
		CoinManager.coin += coinsGet [index];
		CoinManager.UpCoin ();
		effectAddCoin.setTextCoin (coinsGet [index].ToString());
		txtCoin.text = "" + CoinManager.coin;
		SelectModeScript.Instance.setCoin (CoinManager.coin);
//		if (!isInGame) {
////			EffectAddCoin.Instance.showCoin (coinsGet [index]);
////			SelectMapManager.Instance.SetTextCoin ();
//		} else {
//			//GUIManager.Instance.UpdateTextCoin ();
//		}
	}
	public void showInApp ()
	{
		pannelInApp.SetActive (true);
		//	panelAnim.show ();

//		if (!isInGame) {
//			//	StartManager.Instance.statusCoin (true);
//		}
		Time.timeScale = 0;
		BadLogic.pause = true;
	}
	public void hideInApp ()
	{
		pannelInApp.SetActive (false);
		//	panelAnim.hide ();
//		if (!isInGame) {
//			//		StartManager.Instance.statusCoin (false);
//		}
		Time.timeScale = 1;
		BadLogic.pause = false;
	}
}
