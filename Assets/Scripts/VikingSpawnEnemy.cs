using UnityEngine;
using System.Collections;

public class VikingSpawnEnemy : MonoBehaviour
{
	public static VikingSpawnEnemy Instance_;
	public GameObject[] enemies;
	public int maxEnemy;
	public Transform[] transEnemy;
	[HideInInspector]
	public int count = 0;
	int numberEnemyTime;
	public Transform[] posSpawn;
	bool blocked = false;

	void Awake ()
	{
		blocked = true;
		count = 0;
		if (Instance_ == null) {
			Instance_ = this;
		}
		transEnemy = new Transform[maxEnemy];
		for (int i = 0; i < maxEnemy; i++) {
			GameObject go = Instantiate (enemies [randomIndex (enemies.Length)]) as GameObject;
			transEnemy [i] = go.transform.FindChild ("CenterPoint");
			transEnemy [i].GetComponentInParent<EnemyMovement> ().Start ();
		//	SwapColorOnHit[] swap = transEnemy [i].parent.GetComponentsInChildren<SwapColorOnHit> ();
//			foreach (SwapColorOnHit temp in swap) {
//				temp.offCalls ();
//				temp.Start ();
//			}
			transEnemy [i].parent.gameObject.SetActive (false);
		}
		numberEnemyTime = 1;
	}
	void Start ()
	{

		StartCoroutine (delay_resetBlocked ());
	}

	IEnumerator delay_resetBlocked ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
		blocked = false;
	}

	int randomIndex (int max)
	{
		int i = Random.Range (1, max + 1);
		return i - 1;
	}

	public void _GetEnemy (Vector3 pos)
	{
		if (!blocked) {
			if (count < maxEnemy) {
				Transform[] temp = new Transform[numberEnemyTime];
				transEnemy [count].parent.transform.position = positionSpwan (pos);
				transEnemy [count].parent.gameObject.SetActive (true);
				temp [0] = transEnemy [count];
				count += 1;
				CameraScript.Instance.addNewEnemies (numberEnemyTime, temp);
				//	GUIScreenSpace.Instance.setTextTime (count);
			} else {
				if (!BadLogic.lose) {
					BadLogic.win = true;
					EnemiesManager.Instance.StartCoroutine (EnemiesManager.Instance.delayOnWin ());
				}
			}
		}
	}

	int indexMax;

	public Vector3 positionSpwan (Vector3 posPlayer)
	{
		float distance = Vector3.Distance (posPlayer, posSpawn [0].position);
		indexMax = 0;
		for (int i = 1; i < posSpawn.Length; i++) {
			float distancetemp = Vector3.Distance (posPlayer, posSpawn [i].position);
			if (distancetemp > distance) {
				distance = distancetemp;
				indexMax = i;
			}
		}
		return posSpawn [indexMax].position;
	}
}
