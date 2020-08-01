using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public static CameraScript Instance;
	public float maxY;
	public float maxX;
	[HideInInspector]
	public Transform player;
	Vector3 target;
	bool follow = true;
	public EnemiesManager enemiesManager;
	Transform[] enemiesTrans;
	bool styleA = false, blockStyleA = false;
	public OldTVScript OldTV;
	bool offOldTV = false;
	bool modeBoxer = false;


	public void addNewEnemies (int value, Transform[] transTemp)
	{
		if (!styleA) {
			styleA = true;
			blockStyleA = false;
		}
		StartCoroutine (delay_addNewEnemies (value, transTemp));
	}
	IEnumerator delay_addNewEnemies (int value, Transform[] transTemp)
	{
		yield return new WaitForEndOfFrame ();
		enemiesTrans = new Transform[value];
		for (int i = 0; i < value; i++) {
			enemiesTrans [i] = transTemp [i];
		}
	}
	public void slowCam(){
		if (!styleA) {
			styleA = true;
			blockStyleA = false;
		}
	}
	void Start ()
	{
		if (Instance == null) {
			Instance = this;
		}
		player = GameObject.FindGameObjectWithTag ("Player").transform.FindChild("FollowPoint");
		follow = false;
		enemiesTrans = new Transform[enemiesManager.enemies.Length];
		for (int i = 0; i < enemiesManager.enemies.Length; i++) {
			if (!GUIManager.Instance.modeBoxer) {
				enemiesTrans [i] = enemiesManager.enemies [i].transform.FindChild ("CenterPoint");
			} else {
				enemiesTrans [i] = enemiesManager.enemies [i].transform.FindChild ("1Quad_Nect (1)_2_bot");
			}
		}
		styleA = true;
		blockStyleA = false;
		offOldTV = false;
		modeBoxer = GUIManager.Instance.modeBoxer;
	}
	float tempDistance;
	float tempDistanceMax;
	Vector3 targetTemp;
	Vector3 point_Cam_check;
	int idLate = 0;
	float xMax = 0;
	float yMax = 0;
	int countIndex = 0;


	//	void Update ()
	//	{
	//		//SlowMotionScript.Instance.lookedPlayer = check (player.position);
	//	}

	void LateUpdate ()
	{
		if (!BadLogic.pause) {
			if (follow) {
				target = new Vector3 (player.position.x, player.position.y, -10);
//				if (Mathf.Abs (target.x) >= maxX) {
//					if (target.x > 0) {
//						target.x = maxX;
//					} else {
//						target.x = -maxX;
//					}
//				}
//				if (Mathf.Abs (target.y) >= maxY) {
//					if (target.y > 0) {
//						target.y = maxY;
//					} else {
//						target.y = -maxY;
//					}
//				}
				//this.transform.position = Vector3.Lerp (this.transform.position, target, Time.fixedDeltaTime * 2f);
				this.transform.position = target;
			}
			if (!follow) {
				//tempDistance = 100f;
				tempDistanceMax = 0;
				xMax = 0;
				yMax = 0;
				countIndex = 0;
				foreach (Transform transTemp in enemiesTrans) {
					if (transTemp != null) {
						float distance = Vector3.Distance (this.transform.position, transTemp.position);
//						if (distance < tempDistance) {
//							tempDistance = distance;
//							targetTemp = enemiesTrans [i].position;
//							if (idLate != enemiesTrans [i].GetInstanceID ()) {
//								idLate = enemiesTrans [i].GetInstanceID ();
//								if (!styleA) {
//									styleA = true;
//									blockStyleA = false;
//								}
//							}
//						}
						if (distance > tempDistanceMax) {
							tempDistanceMax = distance;
							point_Cam_check = transTemp.position;
						}
						xMax += transTemp.position.x;
						yMax += transTemp.position.y;
						countIndex += 1;
					} else {
						if (!styleA) {
							styleA = true;
							blockStyleA = false;
						}
					}
				}
				countIndex += 1;
				target = new Vector3 ((xMax + player.position.x) / countIndex, (yMax + player.position.y) / countIndex, -10f);
				if (Vector3.Distance (this.transform.position, player.position) > Vector3.Distance (this.transform.position, point_Cam_check)) {
					point_Cam_check = player.position;
				}
				if (styleA) {
					this.transform.position = Vector3.Lerp (this.transform.position, target, 0.05f);
					if (!blockStyleA) {
						blockStyleA = true;
						StartCoroutine (delay_BlockStyleA ());
						if (!offOldTV) {
							OldTV.PlayOldTV ();
							offOldTV = false;
						}
					}
				} else {
					if (!modeBoxer) {
						this.transform.position = target;
					} else {
						this.transform.position = target;
					}
				}
				if (Vector3.Distance (this.transform.position, target) <= 0.05f) {
					contact = true;
				} else {
					contact = false;
				}
				SlowMotionScript.Instance.lookedPlayer = check (point_Cam_check);
				//if (Vector3.Distance (this.transform.position, target) <= 0.01f) {
				//follow = true;
				//	return;
				//	}
			}
		}
	}
	bool contact = true;
	IEnumerator delay_BlockStyleA ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		if (contact) {
			styleA = false;
		} else {
			StartCoroutine (delay_BlockStyleA ());
		}
	}

	Vector3 pos;

	bool check (Vector3 temp)
	{
		pos = Camera.main.WorldToViewportPoint (temp);
		if (pos.y < 0 || pos.x < 0) {
			return false;
		}
		if (pos.y > 0 && pos.y < 0.7f) {
			/// 
			/// 
			if (pos.x < 0.7f && pos.x > 0) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
	public void _RemoveTarger(int id){
		for (int i = 0; i < enemiesTrans.Length; i++) {
			if (enemiesTrans [i] != null) {
				if (enemiesTrans [i].GetInstanceID () == id) {
					enemiesTrans [i] = null;		
				}
			}
		}
	}
}
