using UnityEngine;
using System.Collections;

[System.Serializable]
public class InfoMode
{
	public int maxLevel;
	public string nextModeName;
	public string nameUI;
	public bool unloccked;
}
public class RAMMode
{
	public static string nameMode = "Classic";
	public static string nameLevel = "1";
	public static string nameUI = "Classic";
	public static string keyModeNext ="";
	public static int maxlevel;
	public static bool replay = true;
	public static bool replayLevel;
	public static MODEINFO[] MODEINFOS;
	public static MODEINFO CURMODEINFO;
	public static void CreateClould(MODEINFO[] infos){
		MODEINFOS = infos;
	}
	public static MODEINFO _MODEINFO(string key){
		for (int i = 0; i < MODEINFOS.Length; i++) {
			if (key == MODEINFOS [i].keyMode) {
				CURMODEINFO = MODEINFOS [i];
				break;
			}
		}
		return CURMODEINFO;
	}

//	public static InfoMode temp;

//	public static InfoMode GetInfoMode (string mode)
//	{
//		switch (mode) {
//		case"Classic":
//			temp = new InfoMode ();
//			temp.maxLevel = 7;
//			temp.nextModeName = "Advanced";
//			temp.nameUI = "Adventure";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Advanced":
//			temp = new InfoMode ();
//			temp.maxLevel = 6;
//			temp.nextModeName = "PathOfNinja";
//			temp.nameUI = "Ultimate";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"PathOfNinja":
//			temp = new InfoMode ();
//			temp.maxLevel = 6;
//			temp.nextModeName = "StickOfLaw";
//			temp.nameUI = "Ninja Fight";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"StickOfLaw":
//			temp = new InfoMode ();
//			temp.maxLevel = 10;
//			temp.nextModeName = "Boxer";
//			temp.nameUI = "Justice";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Boxer":
//			temp = new InfoMode ();
//			temp.maxLevel = 12;
//			temp.nextModeName = "DeadlySpikes";
//			temp.nameUI = "Boxer";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"DeadlySpikes":
//			temp = new InfoMode ();
//			temp.maxLevel = 11;
//			temp.nextModeName = "PathOfNinja2";
//			temp.nameUI = "Dangerous Zone";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"PathOfNinja2":
//			temp = new InfoMode ();
//			temp.maxLevel = 10;
//			temp.nextModeName = "Gravity";
//			temp.nameUI = "Ninja Fight 2";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Gravity":
//			temp = new InfoMode ();
//			temp.maxLevel = 8;
//			temp.nextModeName = "Cowboy";
//			temp.nameUI = "Saw Blade";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Cowboy":
//			temp = new InfoMode ();
//			temp.maxLevel = 6;
//			temp.nextModeName = "Viking";
//			temp.nameUI = "Cowboys";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Viking":
//			temp = new InfoMode ();
//			temp.maxLevel = 11;
//			temp.nextModeName = "Viking2";
//			temp.nameUI = "Vikings";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Viking2":
//			temp = new InfoMode ();
//			temp.maxLevel = 12;
//			temp.nextModeName = "Sword";
//			temp.nameUI = "Vikings 2";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		case"Sword":
//			temp = new InfoMode ();
//			temp.maxLevel = 8;
//			temp.nextModeName = "Sword";
//			temp.nameUI = "Star War";
//			temp.unloccked = (PlayerPrefs.GetInt (mode + "unlocked") == 1) ? true : false;
//			return temp;
//		default:
//			return temp;
//		}
//	}
}
