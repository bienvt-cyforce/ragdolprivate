using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{
	public BoosterScript boosterHealth;
	public BoosterScript boosterStrengh;
	public BoosterScript boosterKill;
	[HideInInspector]
	public int[] values = new int[3];
	public static bool UsedSkill = false;
	void Awake ()
	{
//		PlayerPrefs.SetInt ("BOOSTER_HEALTH",10);
//		PlayerPrefs.SetInt ("BOOSTER_STRENGH",10);
//		PlayerPrefs.SetInt ("BOOSTER_KILL",10);
		values [0] = PlayerPrefs.GetInt ("BOOSTER_HEALTH");
		values [1] = PlayerPrefs.GetInt ("BOOSTER_STRENGH");
		values [2] = PlayerPrefs.GetInt ("BOOSTER_KILL"); 



		BadLogic.boosterStengh = false;
		BadLogic.useHealth = false;
		UsedSkill = false;

	}
	void Start(){
		setInfoValues ();
	}
	public void setInfoValues ()
	{
		bool a, b, c;
		boosterHealth.setInfo (values [0]);
		boosterStrengh.setInfo (values [1]);
		boosterKill.setInfo (values [2]);
		a = (values [0] > 0) ? true : false;
		b = (values [1] > 0) ? true : false;
		c = (values [2] > 0) ? true : false;
		PauseGameScript.Instance.setStateBlock (!a, !b, !c);
	}
	public void ActiveBooster (int index)
	{
		switch (index) {
		case 0:
			if (values [index] > 0) {
				if (!BadLogic.useHealth) {
					values [index] -= 1;
					boosterHealth.setInfo (values [index]);
					PlayerPrefs.SetInt ("BOOSTER_HEALTH", values [index]);
					PlayerPrefs.Save ();
					GUIManager.Instance.click_UnPause ();
					PlayerMovement.Instance.resetDameHealth ();
					BadLogic.useHealth = true;
					UsedSkill = true;
					// active Skill
				}
			}
			break;
		case 1:
			if (values [index] > 0) {
				if (!BadLogic.boosterStengh) {
					values [index] -= 1;
					boosterStrengh.setInfo (values [index]);
					PlayerPrefs.SetInt ("BOOSTER_STRENGH", values [index]);
					PlayerPrefs.Save ();
					// active Skill
					GUIManager.Instance.click_UnPause ();
					BadLogic.boosterStengh = true;
					UsedSkill = true;
				}
			}
			break;
		case 2:
			if (values [index] > 0) {
				values [index] -= 1;
				boosterKill.setInfo (values [index]);
				//
				PlayerPrefs.SetInt ("BOOSTER_KILL", values [index]);
				PlayerPrefs.Save ();
				// active Skill
				GUIManager.Instance.click_UnPause ();
				StartCoroutine (delayBOOSTER_KILL ());
				UsedSkill = true;
			}
			break;
		}
	}
	IEnumerator delayBOOSTER_KILL(){
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds(0.5f));
		//print ("WTF - Men?");
		EnemiesManager.Instance.deleteAllEnemy ();
	}
}
