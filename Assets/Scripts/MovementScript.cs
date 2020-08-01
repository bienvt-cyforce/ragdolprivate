using UnityEngine;
using System.Collections;


public class MovementScript : MonoBehaviour
{
	public Rigidbody2D rigi2d;
	public float moveSpeed;
	[HideInInspector]
	public float h, v;
	float angleTemp;
	float speedTemp = 0;
	public Rigidbody2D[] rigi2dAll;
	public float gravity = 1;
	bool isUp = false;
	public float power;
	bool isAdded = false;
	bool blocked = false;
	float distance, distanceLate;
	public float smooth;
	public float timeLockControl, speedRo = 1f;
	EnemyMovement enemyMovemet;
	public bool inranger = false;
	float perVelocity;
	float speedVelocity = 0;
	void Start ()
	{
		rigi2dAll = this.GetComponentsInChildren<Rigidbody2D> ();
		enemyMovemet = this.GetComponent<EnemyMovement> ();
		delayAddForce = false;
		stun = true;
		checkGravity ();
		inranger = false;
		speedRo = 1f;
		if (GUIManager.Instance.modeBoxer) {
			power += 200f;
		}
		speedVelocity = BadLogic.speedVelocity;
		if (speedVelocity == 0)
			speedVelocity = 0.1f;
	}
	public void revivePlayer(){
		delayAddForce = false;
		stun = true;
		checkGravity ();
		speedTemp = 0;
		//angleTemp = 0;
		delayBlock = false;
		addForceFixAI = false;
		isAdded = false;
		delayBlock = false;
		speedRo = 1f;
		countAdd = 0; countAddLate = 0;
	}
	public void SetGravity (float value)
	{
		for (int i = 0; i < rigi2dAll.Length; i++) {
			rigi2dAll [i].gravityScale = value;
		}
		rigi2d.gravityScale *= 2;
	}

	public void setHV (float hTemp, float vTemp)
	{
		h = hTemp;
		v = vTemp;
	}

	void Update ()
	{
		if (isAdded)
			return;
		//if (h != 0 && v != 0) {
		//			angleTemp = Mathf.Atan2 (v, h);
		//			distance = (angleTemp * Mathf.Rad2Deg) - 90;
		//			if (Mathf.Abs (distance - distanceLate) < smooth) {
		//				rigi2d.MoveRotation (distance);
		//				distanceLate = distance;
		//			} 
		//}
		angleTemp = Mathf.Atan2 (v, h) * Mathf.Rad2Deg;
		//		print (angleTemp);
		rigi2d.MoveRotation (rigi2d.rotation + angleTemp * Time.deltaTime * speedRo);
		if (speedTemp < moveSpeed) {
			speedTemp += speedVelocity;
		}
		if (!delayBlock) {
			if (!inranger) {
				if (!delayAddForce) {
					if (Mathf.Abs (rigi2d.velocity.x) < moveSpeed) {
						rigi2d.velocity = new Vector2 (h * speedTemp + rigi2d.velocity.x, rigi2d.velocity.y);
					}
					if (Mathf.Abs (rigi2d.velocity.y) < moveSpeed) {
						rigi2d.velocity = new Vector2 (rigi2d.velocity.x, v * speedTemp + rigi2d.velocity.y);
					}
				}
			}
		}
		//perVelocity = Mathf.Sqrt (rigi2d.velocity.x * rigi2d.velocity.x + rigi2d.velocity.y * rigi2d.velocity.y);
		//if (perVelocity == 0f) {
		//	perVelocity = 0.1f;
		//}
		if (!isAdded && !delayBlock) {
			if (rigi2d.position.y > 0 && !isUp) {
				isUp = true;
				SetGravity (-gravity);
				if (!stun) {
					stun = true;
					StartCoroutine (delayNhan ());
				}
			}
			if (rigi2d.position.y < 0 && isUp) {
				isUp = false;
				SetGravity (gravity);
				if (!stun) {
					stun = true;
					StartCoroutine (delayNhan ());
				}
			}
		}
		if (blocked) {
			speedTemp = 0;
			blocked = false;
			if (!delayBlock) {
				StartCoroutine (block ());
				delayBlock = true;
			}
		}
	}
	public bool addForceFixAI = false;
	IEnumerator delay_addForceFixAI ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		//yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.1f));
		addForceFixAI = false;
	}
	public void add_force_FixAI (Vector2 direction)
	{
		if (!addForceFixAI) {
			direction.Normalize ();
			//yield return new WaitForEndOfFrame ();
			try{
				rigi2d.velocity = new Vector2 (rigi2d.velocity.x * direction.x, rigi2d.velocity.y * direction.y);
				speedTemp = 0;
				rigi2d.AddForce (direction * power/2f * BadLogic.rangerMoveScript);
				StartCoroutine (delay_addForceFixAI ());
				addForceFixAI = true;
			}catch{
			}
		}
		if (enemyMovemet) {
			enemyMovemet.switchDirection ();
		} 
	}
	bool stun = true;
	IEnumerator delayNhan ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		stun = false;
	}
	int countAdd = 0, countAddLate = 0;
	public void add_force (Vector2 direction)
	{
		//StartCoroutine (block ());
		if (!delayAddForce) {
			countAdd += 1;
			delayAddForce = true;
			direction.Normalize ();
			//yield return new WaitForEndOfFrame ();
			rigi2d.velocity = new Vector2 (rigi2d.velocity.x * direction.x, rigi2d.velocity.y * direction.y);
			speedTemp = 0;
			rigi2d.AddForce (direction * power * BadLogic.rangerMoveScript);
			StartCoroutine (_delayAddForce ());
		}
		if (!isAdded) {
			//SetGravity (0);
			countAddLate = countAdd;
			isAdded = true;
			StartCoroutine (delayAdded ());
		}
	}

	IEnumerator _delayAddForce ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.2f));
		//yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.1f));
		yield return new WaitForEndOfFrame();
		delayAddForce = false;
	}
	bool delayAddForce = false;
	bool delayBlock = false;
	public IEnumerator block ()
	{
		SetGravity (0);
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		checkGravity ();
		delayBlock = false;
	}
	IEnumerator delayAdded ()
	{
		yield return new WaitForSeconds (timeLockControl);
		if (countAddLate == countAdd) {
			checkGravity ();
			isAdded = false;
			countAdd = 0;
			countAddLate = 0;
		} else {
			countAddLate = countAdd;
			StartCoroutine (delayAdded ());
		}
	}
	public void checkGravity ()
	{
		if (this.rigi2d.position.y > 0) {
			isUp = true;
			SetGravity (-gravity);
		} else {
			isUp = false;
			SetGravity (gravity);
		}
	}
	void OnCollisionEnter2D (Collision2D coll)
	{
		//		if (coll.gameObject.tag == "platform") {
		//			if (!blocked) {
		//				blocked = true;
		//			}
		//		}
	}
}
