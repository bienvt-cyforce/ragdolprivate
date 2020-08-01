using UnityEngine;
using System.Collections;

[System.Serializable]
public enum MISSION
{
	KILL_ALL,
	DO_NOT_USE_BOOSTER,
	HEALTH_LEFT,
	COMPLETE_IN
}

[System.Serializable]
public class MissionConfig
{
	public string text;
	public bool state;
	public float special;
}

public class MissionManager : MonoBehaviour
{
	
	public static MissionManager Instance;
	public MISSION missA = MISSION.KILL_ALL;
	public MISSION missB = MISSION.DO_NOT_USE_BOOSTER;
	public MISSION missC = MISSION.HEALTH_LEFT;
	public float rate_health_left = 30, completeInSeconds = 15;

	MissionConfig missAConfig;
	MissionConfig missBConfig;
	MissionConfig missCConfig;
	bool aCur, bCur, cCur;
	bool a, b, c;
	public LoadInfoMission[] loadInfo = new LoadInfoMission[2];
	bool missC_is_2 = false;
	string _nameMode, _nameLevel;

	void Awake ()
	{
//		
//		PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionA", 0);
//		PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionB", 0);
//		PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionC", 0);



		aCur = (PlayerPrefs.GetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionA") == 1) ? true : false;
		bCur = (PlayerPrefs.GetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionB") == 1) ? true : false;
		cCur = (PlayerPrefs.GetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionC") == 1) ? true : false;
		loadInfo [0].enabled = false;
		loadInfo [0].enabled = false;

		if (Instance == null) {
			Instance = this;
		}

		a = b = c = false;

		missAConfig = new MissionConfig ();
		missBConfig = new MissionConfig ();
		missCConfig = new MissionConfig ();

		missAConfig.text = "Kill all enemies";
		missAConfig.state = aCur;
		missAConfig.special = 0;

		missBConfig.text = "Don't use any booster";
		missBConfig.state = bCur;
		missBConfig.special = 0;
		if (missC == MISSION.HEALTH_LEFT) {
			missC_is_2 = false;
			missCConfig.text = "> " + rate_health_left + "% health left";
			missCConfig.state = cCur;
			missCConfig.special = rate_health_left;
		} else {
			missC_is_2 = true;
			missCConfig.text = "Complete in " + completeInSeconds + " seconds";
			missCConfig.state = cCur;
			missCConfig.special = completeInSeconds;
		}
		for (int i = 0; i < 2; i++) {
			loadInfo [i].setInfoAll ();
		}
	}

	void Start ()
	{
		loadInfo [0].enabled = false;
		loadInfo [0].enabled = false;
	}

	public MissionConfig infoMissionA ()
	{
		return missAConfig;
	}

	public MissionConfig infoMissionB ()
	{
		return missBConfig;
	}

	public MissionConfig infoMissionC ()
	{
		return missCConfig;
	}

	public void checkMissionCompleted (bool win)
	{
		int star = 0;
		if (!aCur) {
			a = EnemiesManager.Instance.killAllEnemy ();
			if (a) {
				if (win) {
					PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionA", 1);
				}
			}
		} else {
			a = true;
		}
		if (!bCur) {
			b = !BoosterManager.UsedSkill;
			if (b) {
				if (win) {
					PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionB", 1);
				}
			}
		}
		if (!cCur) {
			print ("perHealthLeft@" + PlayerMovement.Instance.perHealthLeft ());
			if (!missC_is_2) {
				c = (PlayerMovement.Instance.perHealthLeft () > rate_health_left) ? true : false;
			} else {
				c = (GUIScreenSpace.Instance.checkCompleteInSeconds ((int)missCConfig.special));
			}
			if (c) {
				if (win) {
					PlayerPrefs.SetInt (RAMMode.nameMode + RAMMode.nameLevel + "MissionC", 1);
				}
			}
		} else {
			c = true;
		}

		if (a || aCur)
			star += 1;
		if (b || bCur)
			star += 1;
		if (c || cCur)
			star += 1;
		print (_nameMode + _nameLevel + "sdasdasdasdasdasdasdas0");
		//print ("giatri" + a + b + c);
		if (win) {
			int i = PlayerPrefs.GetInt (_nameMode + _nameLevel);
			if (i != 4) {
				if (star > i) {
					PlayerPrefs.SetInt (_nameMode + _nameLevel, star);
					PlayerPrefs.Save ();
				}
			} else {
				PlayerPrefs.SetInt (_nameMode + _nameLevel, star);
				PlayerPrefs.Save ();
			}
		}
		if (RAMMode.replay) {
			star *= 10;
			CoinManager.coin += star;
			CoinManager.UpCoin ();
		}
	}

	public void _setAllMisssion (bool win, string nameMode, string nameLevel)
	{
		_nameMode = nameMode;
		_nameLevel = nameLevel;
		checkMissionCompleted (win);
		for (int i = 0; i < 2; i++) {
			loadInfo [i].SetCompletedMission (a, b, c);
		}
	}
}
