using UnityEngine;
using System.Collections;

public class LoadInfoMission : MonoBehaviour {
	public MissionScript missA;
	public MissionScript missB;
	public MissionScript missC;
	public void setInfoStart(ref MissionScript mS,MissionConfig temp){
		mS.setInfo (temp.text);
		mS.setState (temp.state);
	}
	public void SetCompletedMission(bool stateA,bool stateB,bool stateC){
		missA.setState (stateA);
		missB.setState (stateB);
		missC.setState (stateC);
	}
	public void setInfoAll(){
		setInfoStart (ref missA, MissionManager.Instance.infoMissionA());
		setInfoStart (ref missB, MissionManager.Instance.infoMissionB());
		setInfoStart (ref missC, MissionManager.Instance.infoMissionC());
	}
}
