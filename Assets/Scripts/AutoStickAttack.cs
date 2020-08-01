using UnityEngine;
using System.Collections;

public class AutoStickAttack : MonoBehaviour
{
	StickWeaponScript stick;
	string tagg;

	void Start ()
	{
		stick = this.GetComponentInParent<StickWeaponScript> ();
	}
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (!stick.attacked) {
			tagg = (stick.ofPlayer) ? "SLOWTRIGGER" : "SLOW_PLAYER";
			if (coll.gameObject.tag == tagg) {
				Vector2 direction = new Vector2 (coll.transform.position.x, coll.transform.position.y) - new Vector2 (this.transform.position.x,this.transform.position.y);
				stick.attack_Stick (direction, new Vector2 (this.transform.position.x, this.transform.position.y));
			}
		}
	}
}
