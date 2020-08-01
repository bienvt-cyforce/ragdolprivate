using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoSetInfoLevel : MonoBehaviour
{
	public Text[] txtMode;
	public Text[] txtLevel;

	void Start ()
	{
		string nameMode = RAMMode.nameMode;
		string nameLevel = RAMMode.nameLevel;
		string nameUI = RAMMode._MODEINFO(nameMode).nameUI;
		for (int i = 0; i < txtMode.Length; i++) {
			txtMode [i].text = nameUI + " " + nameLevel;
		}
		for (int i = 0; i < txtLevel.Length; i++) {
			txtLevel [i].text = "Level " + nameLevel;
		}
	}
}
