using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
	public int health;
	[HideInInspector]
	public int dame = 0;
	bool isPlayer;
	[HideInInspector]
	public MovementScript movement;
	[HideInInspector]
	public bool deathed = false;
	Transform centerTrans;

	public SpriteRenderer[] SPR_S_C = new SpriteRenderer[2];
	Color32 colorDame;
	public bool endlessMode;
	AutoCheckOut autoCheckOut;
	Vector3 posDieLate;
	Transform headTrans;

	public void changeHealth (int value)
	{
		dame += value;
		//	print (gameObject.tag + " " + dame);
		colorDame.a = (byte)((perCurDame () / 100f) * 255f);
		changeColorWithPerDame ();
		checkHealth ();
	}

	FixedJoint2D[] fixedJoint2D;
	HingeJoint2D[] hingeJoint2D;
	SwapColorOnHit[] swapColorOnHits;

	[HideInInspector]
	void Awake ()
	{
		deathed = false;
		fixedJoint2D = this.GetComponentsInChildren<FixedJoint2D> ();
		hingeJoint2D = this.GetComponentsInChildren<HingeJoint2D> ();
		swapColorOnHits = this.GetComponentsInChildren<SwapColorOnHit> ();
		movement = this.GetComponent<MovementScript> ();
		colorDame = this.GetComponent<SetupScript> ().colorHit;
		//endlessMode = GameObject.FindObjectOfType<GUIManager> ().EndlessMode;
	}

	void Start ()
	{
		isPlayer = (this.GetComponent<SetupScript> ().nameLayer_body == "Player") ? true : false;
		if (isPlayer) {
			centerTrans = this.transform.FindChild ("FollowPoint");
		} else {
			centerTrans = this.transform.FindChild ("CenterPoint");
		}
		autoCheckOut = centerTrans.GetComponent<AutoCheckOut> ();
		//colorDame = SPR_S_C [0].color;
		colorDame = this.GetComponent<SetupScript> ().colorHit;
		colorDame.a = 0;
		for (int i = 0; i < 2; i++) {
			SPR_S_C [i].color = colorDame;
			SPR_S_C [i].sortingOrder = 3;
		}
		//changeColorWithPerDame ();
		headTrans = transform.FindChild ("HeadPlayer");
	}

	public int perCurDame ()
	{
		int per = (int)(((float)(dame) / (float)health) * 100f);
		return per;
	}

	public void changeColorWithPerDame ()
	{
		for (int i = 0; i < 2; i++) {
			SPR_S_C [i].color = colorDame;
		}
	}

	public int perHealthLeft ()
	{
		int per = (int)(((float)(health - dame) / (float)health) * 100f);
		//print ("@@@@@"+(float)dame/(float)health);
		return per;
	}

	public void checkHealth ()
	{
		if (dame >= health) {
			// die
			death ();
		}
	}

	public void death ()
	{
		if (!deathed) {
			//		movement.rigi2d.isKinematic = true;
			posDieLate = this.transform.position;
			SoundManager.Instance.playClip_Dead ();
			SlowMotionScript.Instance._SlowOnHit ();
			SlowMotionScript.Instance._slowOnDeath ();
			movement.enabled = false;
			if (isPlayer) {
				if (!BadLogic.win) {
					BadLogic.lose = true;
					StartCoroutine (delayGameover ());
				}
			} else {
				if (!endlessMode) {
					EnemiesManager.Instance.removeEnemy_ (this.transform.position);
					Destroy (this.gameObject, 4f);
				} else {
					StartCoroutine (delay_getEnemy ());
					Destroy (this.gameObject, 2f);
				}
			}
			//	EnemiesManager.Instance.checkEnemyLive ();
			StartCoroutine (_delay ());
			deathed = true;
			CameraScript.Instance._RemoveTarger (this.centerTrans.GetInstanceID ());
		}
	}

	IEnumerator delay_getEnemy ()
	{
		yield return new WaitForSecondsRealtime (1f);
		EndlessModeManager.Instance_._GetEnemy (CameraScript.Instance.player.position);
	}

	IEnumerator delayGameover ()
	{
		if (!GUIManager.Instance.TwoPlayerSolo) {
			GUIScreenSpace.Instance.setTextOnLose ();
		} else {
			GUIScreenSpace.Instance.setTextSoloMode ("1");
		}
		GUIScreenSpace.Instance.btnPause.SetActive (false);
		yield return new WaitForSecondsRealtime (5f);
		// GameOver!
		GUIManager.Instance.OnLose ();
	}

	IEnumerator _delay ()
	{
		autoCheckOut._findParentHead ();
		yield return new WaitForEndOfFrame ();
		HitCircleBMan.Instance.play (centerTrans.position);
		yield return new WaitForEndOfFrame ();
		HitBMan.Instance.play (centerTrans.position);
		yield return new WaitForEndOfFrame ();
		for (int i = 0; i < fixedJoint2D.Length; i++) {
			fixedJoint2D [i].enabled = false;
			//fixedJoint2D [i].transform.parent = null;
		}
		yield return new WaitForEndOfFrame ();
		for (int i = 0; i < hingeJoint2D.Length; i++) {
			hingeJoint2D [i].enabled = false;
			//hingeJoint2D [i].transform.parent = null;
		}
		yield return new WaitForEndOfFrame ();
		float time = (endlessMode) ? 3f : 4f;
		for (int i = 0; i < swapColorOnHits.Length; i++) {
			swapColorOnHits [i].add_froceOnDeath ();
			if (!isPlayer) {
				swapColorOnHits [i].transform.parent = null;
			}
			swapColorOnHits [i].enabled = false;
			swapColorOnHits [i].BlockDame_Death ();
			if (!isPlayer) {
				swapColorOnHits [i].Destroythisgo (time);
			}
		}
		SlowMotionScript.Instance.slowing = false;
	}

	public void resetDame ()
	{
		dame = 0;
		colorDame.a = 0;
		changeColorWithPerDame ();
		checkHealth ();
	}

	public bool inRevive = false;
	Vector3 posHead;

	public void RevivePlayer ()
	{
		
		StartCoroutine (delay_revivePlayer ());
	}

	float[] valueInertia;

	IEnumerator delay_revivePlayer ()
	{
		yield return new WaitForSecondsRealtime (0.5f);
		CameraScript.Instance.slowCam ();
		SlowMotionScript.Instance.slowing = true;
		inRevive = true;
		//this.transform.position = Vector3.zero;
		//	movement.rigi2d.velocity = Vector2.zero;
		//	movement.rigi2d.freezeRotation = true;
		//	movement.rigi2d.isKinematic = true;
		posHead = Vector3.zero;
		this.transform.position = posHead;
		headTrans.localPosition = Vector3.zero;
		//	movement.rigi2d.isKinematic = true;
		//	transform.FindChild ("HeadPlayer").localPosition = new Vector3 (0, 0, 0);
		autoCheckOut.findParentPlayer ();
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("Player1"), true);
		resetDame ();
		//movement.enabled = true;
		for (int i = 0; i < hingeJoint2D.Length; i++) {
			hingeJoint2D [i].enabled = true;
			//hingeJoint2D [i].transform.parent = null;
		}
		for (int i = 0; i < fixedJoint2D.Length; i++) {
			fixedJoint2D [i].enabled = true;
			//fixedJoint2D [i].transform.parent = null;
		}
		valueInertia = new float[swapColorOnHits.Length];
		for (int i = 0; i < swapColorOnHits.Length; i++) {
			swapColorOnHits [i].rg2d.velocity = Vector2.zero;
			valueInertia [i] = swapColorOnHits [i].rg2d.inertia;
			swapColorOnHits [i].rg2d.inertia = 1f;
		}
		yield return new WaitForSecondsRealtime (0.25f);
		SlowMotionScript.Instance.slowing = true;

		yield return new WaitForSecondsRealtime (6f);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("Player1"), false);
		deathed = false;
		for (int i = 0; i < swapColorOnHits.Length; i++) {
			swapColorOnHits [i].enabled = true;
			swapColorOnHits [i].revivePlayer ();
			swapColorOnHits [i].rg2d.inertia = valueInertia [i];
		}
		//	movement.rigi2d.freezeRotation = false;
		inRevive = false;
		GUIScreenSpace.Instance.GUIPlay.SetActive (true);
//		movement.rigi2d.isKinematic = false;

		movement.revivePlayer ();
		movement.enabled = true;

	}

	void Update ()
	{
		if (inRevive) {
			//	movement.rigi2d.MoveRotation (0f);
			this.transform.position = posHead;
			headTrans.localPosition = Vector3.zero;
		}
	}
}
