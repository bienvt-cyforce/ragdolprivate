using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public MovementScript movement;
	Transform transPlayer;
	Rigidbody2D rigi2d;
	HealthScript targetMovement;
	public float rangeAttack;
	float distance;
	Vector3 direction;
	Transform pointCenter;
	bool inRanger = true;
	bool switchDirec = false;
	bool moveBackward = false;
	bool move90 = false;
	bool modeBoxer = false;
	bool callStart = false;
	public void Start ()
	{
		if (!callStart) {
			pointCenter = this.transform.FindChild ("CenterPoint");
			targetMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthScript> ();
			rigi2d = this.GetComponent<Rigidbody2D> ();
			modeBoxer = GameObject.FindObjectOfType<GUIManager> ().modeBoxer;
			if (!modeBoxer) {
				transPlayer = GameObject.FindGameObjectWithTag ("Player").transform.FindChild ("FollowPoint").transform;
			} else {
				transPlayer = GameObject.FindGameObjectWithTag ("Player").transform;
			}
		}
		callStart = true;
	}

	void Update ()
	{
		if (!targetMovement.deathed) {
			direction = (transPlayer.position - pointCenter.position);
			direction.Normalize ();
			if (!switchDirec) {
				if (!inRanger) {
					movement.setHV (direction.x, direction.y);
				} else {
					if (!modeBoxer) {
						movement.setHV (-direction.x, -direction.y);
					//	rigi2d.velocity = Vector2.zero;
					} else {
						switchDirection ();
					}
				}
			} else {
				if (moveBackward) {
					direction = -1f * direction;
				}
				if (move90) {
					if (Mathf.Abs (direction.x) < Mathf.Abs (direction.y)) {
						direction.x = -1f * direction.x;
					} else {
						direction.y = -1f * direction.y;
					}
				}
				movement.setHV (direction.x, direction.y);
			}
		}
		if (transPlayer) {
			distance = Vector3.Distance (pointCenter.position, transPlayer.position);
		}
		if (distance <= rangeAttack) {
			inRanger = true;
		} else {
			inRanger = false;
		}
		if (modeBoxer) {
			movement.inranger = inRanger;
		}
	}

	bool blockSwitch = false;

	public void switchDirection ()
	{
		if (!blockSwitch) {
			if (!switchDirec) {
				switchDirec = true;
				StartCoroutine (delay_swithDirection ());
			}
		}
	}

	IEnumerator delay_swithDirection ()
	{
		blockSwitch = true;
		moveBackward = true;
		move90 = !moveBackward;
		yield return new WaitForSecondsRealtime (0.5f);
		moveBackward = false;
		move90 = !moveBackward;
		yield return new WaitForSecondsRealtime (1f);
		switchDirec = move90 = moveBackward = false;
		yield return new WaitForSecondsRealtime (4f);
		blockSwitch = false;
	}
}