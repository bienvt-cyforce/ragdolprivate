using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CnControls;

public class SettingCanvasByHu3 : MonoBehaviour
{
	// Update is called once per frame
	public RectTransform JoystickMain;
	public SimpleJoystick joystick;
	public void Start(){
		//img.position = JoystickMain.position;
	}
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			//Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 pos = Input.mousePosition;
			JoystickMain.position = pos;
		}
	}
}
