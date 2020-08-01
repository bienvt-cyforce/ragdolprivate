using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionScript : MonoBehaviour
{
	public Text txtMission;
	public CompleteMissionScript completedMission;
	public void setInfo (string mission)
	{
		txtMission.text = "" + mission;
	}
	public void setState (bool state)
	{
		//print (gameObject.name + "@" + state);
		completedMission.changeStatus (state);
	}
}
