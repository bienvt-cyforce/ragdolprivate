using UnityEngine;
using System.Collections;
using System.Net;

public class OptionManager : MonoBehaviour
{
	bool isJoyRight = false;
	public GameObject JoyLeft, btnLeft, JoyRight, btnRight,GUI_OPJoy;
	void Start ()
	{
		GUI_OPJoy.SetActive (false);
		isJoyRight = (PlayerPrefs.GetInt ("JOY_LEFT") == 0) ? false : true;
		JoyRight.SetActive (!isJoyRight);
		JoyLeft.SetActive (isJoyRight);
		btnLeft.SetActive (!isJoyRight);
		btnRight.SetActive (isJoyRight);
	}
	public void clickActiveLeft ()
	{
		JoyLeft.SetActive (false);
		JoyRight.SetActive (true);
		PlayerPrefs.SetInt ("JOY_LEFT", 0);
		PlayerPrefs.Save ();
		btnLeft.SetActive (true);
		btnRight.SetActive (false);
	}

	public void clickActiveRight ()
	{
		JoyRight.SetActive (false);
		JoyLeft.SetActive (true);
		PlayerPrefs.SetInt ("JOY_LEFT", 1);
		PlayerPrefs.Save ();
		btnRight.SetActive (true);
		btnLeft.SetActive (false);
	}
	public void clickOpJoy(){
		GUI_OPJoy.SetActive (true);
	}
	public void clickBack(){
		GUI_OPJoy.SetActive (false);
	}
}
