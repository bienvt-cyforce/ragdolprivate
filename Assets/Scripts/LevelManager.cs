using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;
	public string key;
	public ButtonLevel[] btnLevels = new ButtonLevel[15];
	public Text Title;
	string nameUI;
	int maxLevel;
	void Awake ()
	{
		Instance = this;
	}
	public void _setInfo (MODEINFO TEMP)
	{
		key = TEMP.keyMode;
		maxLevel = TEMP.maxLevel;
		nameUI = TEMP.nameUI;
		for (int i = 0; i < btnLevels.Length; i++) {
			btnLevels [i]._LoadInfo (i+1,key);
			if (i > maxLevel - 1) {
				btnLevels [i].gameObject.SetActive (false);
			} else {
				btnLevels [i].gameObject.SetActive (true);
			}
		}
		Title.text = nameUI;
	}


//	public void LoadInfoWithIndex (int index)
//	{
////		for (int i = 0; i < btnLevels.Length; i++) {
////			if (i == (index - 1)) {
////				btnLevels [i]._LoadInfo ();
////			}
////		}
//	}
}
