using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonGetCoin : MonoBehaviour
{
	public string day;
	public string coin;
	public Text txtTitle;
	public Text txtCoin;
	public Button btn;
	public GameObject block;
	bool coinGeted = false;
	public void _Start(string nameDay,int _coin)
	{
		day = nameDay;
		coin = _coin.ToString ();
		btn.onClick.AddListener (delegate {
			onClickBtn ();
		});
		txtCoin.text = "" + coin;
		txtTitle.text = "" + day;
	}

	public void _blockButton (bool state)
	{
		block.SetActive (state);
	}

	public void onClickBtn ()
	{
		if (!coinGeted) {
			coinGeted = true;
			PlayerPrefs.SetInt (day, 1);
			PlayerPrefs.Save ();
			DailyManager.Instance.resetAlldayly (day);
			CoinManager.coin += int.Parse(coin);
			CoinManager.UpCoin ();
			DailyManager.Instance.showTextCoinEffect ();

			_blockButton (true);
			StartCoroutine (delay ());
		}
	}

	public bool check__ ()
	{
		return coinGeted = (PlayerPrefs.GetInt (day) == 1) ? true : false;
	}
	IEnumerator delay(){
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1.5f));
		DailyManager.Instance.clickBackDaily ();
	}
}
