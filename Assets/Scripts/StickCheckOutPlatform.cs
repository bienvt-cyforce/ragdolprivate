using UnityEngine;
using System.Collections;

public class StickCheckOutPlatform : MonoBehaviour {
	StickWeaponScript stick;
	void Start(){
		stick = this.GetComponentInParent<StickWeaponScript> ();
	}
	void OnTriggerExit2D (Collider2D coll)
	{
		if (stick.unIgnore) {
			if (coll.gameObject.tag == "platform") {
				StartCoroutine (delay_CheckOut ());
				stick.unIgnore = false;
			}
		}
	}
	IEnumerator delay_CheckOut(){
		yield return new WaitForSecondsRealtime (5f);
		if (stick.used) {
			if (stick.ofPlayer) {
				//Physics2D.IgnoreLayerCollision (stick.wp_maskOfPlayer, stick.MaskLimit, false);
			} else {
				//Physics2D.IgnoreLayerCollision (stick.wp_maskOfEnemy, stick.MaskLimit, false);
			}
		}
	}
}
