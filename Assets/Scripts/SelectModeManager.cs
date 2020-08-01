using UnityEngine;
using System.Collections;

[System.Serializable]
public class MODEINFO
{
	public string keyMode;
	public string nameUI;
	public int maxLevel;
	public int price;
	public Sprite imageMode;
	[HideInInspector]
	public int indexMode;
	[HideInInspector]
	public string keyNext;
}

public class SelectModeManager : MonoBehaviour
{
	public static SelectModeManager Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	public ButtonMode[] bttnMode;
	public MODEINFO[] modeINFO;

	public void Start ()
	{
		RAMMode.CreateClould (modeINFO);
		int total = 0;
		int length = bttnMode.Length;
		for (int i = 0; i < length; i++) {
			bttnMode [i]._SETUP (modeINFO [i]);
			if (i < length - 1) {
				modeINFO [i].keyNext = modeINFO [i + 1].keyMode;
			}
			total += bttnMode [i].numStarActive;
		}
		CoinManager.totalStar = total;
		CoinManager.UpTotalStar ();
	}

	public MODEINFO GetInfoMode (string key)
	{
		MODEINFO temp = new MODEINFO ();
		for (int i = 0; i < bttnMode.Length; i++) {
			if (modeINFO [i].keyMode == key) {
				temp = modeINFO [i];
				break;
			}
		}
		return temp;
	}
	public string GetKeyNextMode (string key)
	{
		return GetInfoMode (key).keyNext;
	}
}
