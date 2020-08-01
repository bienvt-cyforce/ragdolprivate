using UnityEngine;
using System.Collections;

public class EnemiesManager : MonoBehaviour
{
	public static EnemiesManager Instance;
	public GameObject[] enemies;
	int numTotalEnemy;
	bool endlessMode;
	bool modeSolo;
	public bool SpawnSmooth = false;

	void OnEnable ()
	{
		Instance = this;
	}

	void Awake ()
	{
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		coinTemp = 0;
		delaytakecoin = false;
		GUIManager temp = GameObject.FindObjectOfType<GUIManager> ();
		endlessMode = temp.EndlessMode;
		modeSolo = temp.TwoPlayerSolo;
	}

	void Start ()
	{
		numTotalEnemy = enemies.Length;
	}

	int coinTemp = 0;

	public void removeEnemy_ (Vector3 pos)
	{
		if (!endlessMode) {
			numTotalEnemy -= 1;
			if (numTotalEnemy <= 0) {
				if (!SpawnSmooth) {
					if (!BadLogic.lose) {
						BadLogic.win = true;
						StartCoroutine (delayOnWin ());
					}
				} else {
					VikingSpawnEnemy.Instance_._GetEnemy (PlayerMovement.Instance.transform.position);
				}
			}
			if (!delaytakecoin) {
				if (coinTemp <= 20) {
					coinTemp += 10;
				}
				StartCoroutine (delayTakeCoin ());
			}
			CoinManager.coin += coinTemp;
			TextCoinManager.Instance.playEffect (pos, coinTemp.ToString ());
			GUIScreenSpace.Instance.setCoin (CoinManager.coin);
			CoinManager.UpCoin ();
		}
	}

	bool delaytakecoin = false;

	IEnumerator delayEffectCoin ()
	{
		yield return new WaitForEndOfFrame ();
	}

	IEnumerator delayTakeCoin ()
	{
		yield return new WaitForSeconds (3f);
		delaytakecoin = false;
	}

	public IEnumerator delayOnWin ()
	{
		//BadLogic.pause = true;


		GUIScreenSpace.Instance.btnPause.SetActive (false);
		if (!modeSolo) {
			if (!GUIManager.Instance.endMode) {
				GUIScreenSpace.Instance.setTextOnWin ();
			} else {
				GUIScreenSpace.Instance.setTextOnCompletedMode (RAMMode.nameUI + "\r\n" + "Completed!");
			}
		} else {
			GUIScreenSpace.Instance.setTextSoloMode ("2");
		}
		yield return new WaitForSecondsRealtime (5f);
		GUIManager.Instance.OnCompleted ();
	}

	public bool killAllEnemy ()
	{
		return BadLogic.win;
	}

	public bool checkEnemyLive ()
	{
		for (int i = 0; i < enemies.Length; i++) {
			if (enemies [i] != null) {
				hp = enemies [i].GetComponent<HealthScript> ();
				if (!hp.deathed) {
					return false;
				} else {
					return true;
				}
			} else {
				return false;
			}
		}
		return true;
	}

	HealthScript hp;
	GameObject[] emiesFindNew;

	public void findeEnemies ()
	{
		emiesFindNew = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	public void deleteAllEnemy ()
	{
		for (int i = 0; i < emiesFindNew.Length; i++) {
			if (emiesFindNew [i] != null) {
				hp = emiesFindNew [i].GetComponent<HealthScript> ();
				if (!hp.deathed) {
					hp.death ();
				}
			}
		}
	}
}
