using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;
	public MovementScript movement;
	HealthScript healthPlayer;
	void Start(){
		Instance = this;
		healthPlayer = this.GetComponent<HealthScript> ();
	}
	void Update(){
		Vector2 hv;
		hv.x = CnInputManager.GetAxis ("Horizontal");
		hv.y = CnInputManager.GetAxis ("Vertical");
		movement.setHV (hv.x,hv.y);
	}
////	void OnTriggerStay2D(Collider2D coll){
////		if (coll.gameObject.tag == "Enemy") {
////			//SlowMotionScript.Instance._slowMotion ();
////			SlowMotionScript.Instance.slowing = true;
////		}
////	}
//	void OnTriggerEnter2D(Collider2D coll){
//		if (coll.gameObject.tag == "SLOWTRIGGER") {
//			//SlowMotionScript.Instance._slowMotion ();
//			SlowMotionScript.Instance.slowing = true;
//		}
//	}
//	void OnTriggerExit2D(Collider2D coll){
//		if (coll.gameObject.tag == "SLOWTRIGGER") {
//			SlowMotionScript.Instance.slowing = false;
//		}
//	}

	public int perHealthLeft(){
		return healthPlayer.perHealthLeft ();
	}
	public void resetDameHealth(){
		healthPlayer.resetDame ();
	}
	public void revivePlayer(){
		healthPlayer.RevivePlayer ();
	}
}
