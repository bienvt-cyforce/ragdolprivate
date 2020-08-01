using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoloModeManager : MonoBehaviour {
	public static SoloModeManager Instance;
	public GUIManager guiMan;
	public GameObject SoloModeEndgame,SoloModePauseGame;
	public GameObject CanvasPlayer2;
	public Text txtTitle;
	public void Start(){
		if (Instance == null)
			Instance = this;
		SoloModeEndgame.SetActive (false);
		SoloModePauseGame.SetActive (false);
	}
	public void click_Menu(){
		SoloModeEndgame.SetActive (false);
		guiMan.ToMainMenu ();
	}
	public void click_Retry(){
		SoloModeEndgame.SetActive (false);
		guiMan.click_Restart ();
	}
	public void click_Resume(){
		SoloModePauseGame.SetActive (false);
		guiMan.click_UnPause ();
		CanvasPlayer2.SetActive (true);
	}
	public void click_Reset(){
		SoloModeEndgame.SetActive (false);
		guiMan.click_Restart ();
	}
	public void clickPause(){
		SoloModePauseGame.SetActive (true);
		SoloModeEndgame.SetActive (false);
		CanvasPlayer2.SetActive (false);
	}
	public void _OnEndGame(string numberPlayer){
		txtTitle.text="player "+numberPlayer+" win !";
		SoloModeEndgame.SetActive (true);
		SoloModePauseGame.SetActive (false);
		CanvasPlayer2.SetActive (false);
		GUIScreenSpace.Instance.GUIPlay.SetActive (false);
	}
}
