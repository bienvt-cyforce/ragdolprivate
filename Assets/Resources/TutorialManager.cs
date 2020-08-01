using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	public GameObject joystickTip, ControlTip, btnNext;
	public GameObject panelBlockLeft, panelBlockRight;
	public Image statusA, statusB;
	public Sprite[] spr = new Sprite[2];
	bool inTipJoy = false;
	bool isJoyLeft = false;

	void Start ()
	{
		isJoyLeft = (PlayerPrefs.GetInt ("JOY_LEFT") == 0) ? true : false;
		this.TipJoystick ();
		if (isJoyLeft) {
			this.setJoyLeft ();
		} else {
			this.setJoyRight ();
		}
	}

	public void TipJoystick ()
	{
		inTipJoy = true;
		joystickTip.SetActive (true);
		ControlTip.SetActive (false);
		statusA.sprite = spr [0];
		statusB.sprite = spr [1];
		btnNext.SetActive (true);
	}

	public void TipControl ()
	{
		inTipJoy = false;
		joystickTip.SetActive (false);
		ControlTip.SetActive (true);
		statusA.sprite = spr [1];
		statusB.sprite = spr [0];
		btnNext.SetActive (false);
	}

	public void dragging ()
	{
		inTipJoy = !inTipJoy;
		if (inTipJoy) {
			this.TipJoystick ();
		} else {
			this.TipControl ();
		}
	}

	public void setJoyLeft ()
	{
		isJoyLeft = true;
		this.saveDateJoyLeft (true);
		panelBlockLeft.SetActive (false);
		panelBlockRight.SetActive (true);
	}

	public void setJoyRight ()
	{
		isJoyLeft = false;
		this.saveDateJoyLeft (false);
		panelBlockLeft.SetActive (true);
		panelBlockRight.SetActive (false);
	}

	public void saveDateJoyLeft (bool state)
	{
		int value = (state) ? 0 : 1;
		PlayerPrefs.SetInt ("JOY_LEFT", value);
		PlayerPrefs.Save ();
		this.SendInfoToGUI ();
	}

	public void SendInfoToGUI ()
	{
		GUIManager.Instance.joystickIsLeft = !isJoyLeft;
		GUIManager.Instance.setInfoJoy ();
	}

	public void clickDone ()
	{
		Destroy (this.gameObject);
		GUIManager.Instance.MissionPanel.SetActive (true);
	}

	public void NeverShowAgain ()
	{
		Destroy (this.gameObject);
		PlayerPrefs.SetInt ("NEVERSHOWAGAIN", 1);
		PlayerPrefs.Save ();
		GUIManager.Instance.MissionPanel.SetActive (true);
	}
}
