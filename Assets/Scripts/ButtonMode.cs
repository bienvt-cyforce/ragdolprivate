using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMode : MonoBehaviour
{
	public string key;
	public Text info;
	public int numStarActive;
	public Button bttnMode, bttnUnlock;
	public Text txtPriceUnlock;
	public GameObject imgUnlock;
	public int priceUnlock;
	public Color[] colors;
	bool unlocked = false;
	public Text txtTitle;
	public Image imageMap;
	int maxLevel;
	string nameUI;
	MODEINFO CURmodeInfo;


	public void Awake(){
		bttnUnlock.onClick.AddListener (delegate {
			onClick_bttnUnlock ();	
		});
	}
	public void OnClickButton ()
	{
		RAMMode.maxlevel = maxLevel;
		LevelManager.Instance._setInfo (CURmodeInfo);
		StartManager.Instance.activeLevelManager ();
	}

	public void _SETUP (MODEINFO TEMP)
	{
		this.CURmodeInfo = TEMP;
		key = TEMP.keyMode;
		imageMap.sprite = TEMP.imageMode;
		priceUnlock = TEMP.price;
		maxLevel = TEMP.maxLevel;
		nameUI = TEMP.nameUI;

		//txtTitle = this.transform.FindChild ("Title").GetComponent<Text> ();
		//bttnMode = this.GetComponent<Button> ();
		//bttnUnlock = this.transform.FindChild ("ButtonUnlock").GetComponent<Button> ();
		//txtPriceUnlock = this.transform.FindChild ("ButtonUnlock").FindChild("txtPrice").GetComponent<Text> ();
		//imgUnlock = this.transform.FindChild ("imgBlock").gameObject;

		txtPriceUnlock.text = "" + priceUnlock;

		//	info = this.transform.FindChild ("infoStar").GetComponent<Text> ();
		info.text = ("" + getTotalNumStarMode (maxLevel) + "/" + (maxLevel * 3));
		unlocked = (PlayerPrefs.GetInt (key + "unlocked") == 1) ? true : false;
		SetStatus (unlocked);
		txtTitle.text = nameUI;
		if (unlocked) {
			txtTitle.color = colors [0];
		} else {
			txtTitle.color = colors [1];
		}
	}
	public void SetStatus (bool statusUnlock)
	{
		bttnMode.enabled = statusUnlock;
		bttnUnlock.gameObject.SetActive (!statusUnlock);
		txtPriceUnlock.gameObject.SetActive (!statusUnlock);
		imgUnlock.SetActive (!statusUnlock);
	}

	public int getTotalNumStarMode (int level)
	{
		int numstar = 0;
		for (int i = 0; i < level; i++) {
			int tempNumStar = PlayerPrefs.GetInt (key + "" + (i + 1));
			if (tempNumStar != 4) {
				numstar += tempNumStar;
			}
		}
		numStarActive = numstar;
		return numstar;
	}

	public void onClick_bttnUnlock ()
	{
		if (!unlocked) {
			if (checkUnlocked ()) {
				CoinManager.coin -= priceUnlock;
				CoinManager.UpCoin ();
				SelectModeScript.Instance.setCoin (CoinManager.coin);
				// mo khoa level
				PlayerPrefs.SetInt (key + "unlocked", 1);
				PlayerPrefs.SetInt (key + "" + 1, 4);
				PlayerPrefs.Save ();
				unlocked = true;
				txtTitle.color = colors [0];
				SetStatus (unlocked);
			} else {
				Debug.Log ("Khong du tien unlock mode");
			}
		}
	}

	public bool checkUnlocked ()
	{
		if (CoinManager.coin >= priceUnlock) {
			return true;
		} else {
			return false;
		}
	}
}
