using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class SpeedgameScript : MonoBehaviour
{
	public Slider slider;
	public Text txtInfo;
	void Start(){
		float valueCur = PlayerPrefs.GetFloat ("GAMESPEED");
		slider.value = valueCur;
		txtInfo.text = "gamespeed: " + valueCur;
	}
	public void onChangeValue ()
	{
		float value = slider.value;
		value = (float)Math.Round ((double)value,1);
		txtInfo.text = "gamespeed: " + value;
		PlayerPrefs.SetFloat ("GAMESPEED", value);
		PlayerPrefs.Save ();
	}
}
