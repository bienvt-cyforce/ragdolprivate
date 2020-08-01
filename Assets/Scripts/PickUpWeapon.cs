using UnityEngine;
using System.Collections;

public class PickUpWeapon : MonoBehaviour
{
	PickUpWeapon Instance;
	public bool picked = false;
	StickWeaponScript stickWeapon;
	Rigidbody2D rg2d;
	public Transform pointWeapon;
	string layerName;
	bool ofPlayer = false;
	MovementScript movement;
	HealthScript health;
	SwapColorOnHit swapColor;
	public bool isLeft = false;
//	public PickUpWeapon duelPickUp;
	public GameObject triggerFixWeapon;
	void Start ()
	{
		Instance = this;
		rg2d = this.GetComponent<Rigidbody2D> ();
		picked = false;
		layerName = this.GetComponentInParent<SetupScript> ().fix_nameOfLayer;
		ofPlayer = (this.GetComponentInParent<SetupScript> ().nameLayer_body == "Player") ? true : false;
		movement = this.GetComponentInParent<MovementScript> ();
		health = this.GetComponentInParent<HealthScript> ();
		swapColor = this.GetComponent<SwapColorOnHit> ();
		triggerFixWeapon = Instantiate(Resources.Load<GameObject> ("TriggerFixWeapon")) as GameObject;
		triggerFixWeapon.tag = this.gameObject.tag;
		triggerFixWeapon.transform.position = pointWeapon.position;
		triggerFixWeapon.transform.parent = this.transform;
		triggerFixWeapon.SetActive (false);
	}
	public void check_(){
		triggerFixWeapon.SetActive (true);
		this.StartCoroutine (delayCheck ());
	}
	IEnumerator delayCheck(){
		yield return new WaitForSecondsRealtime (0.5f);
		triggerFixWeapon.SetActive (false);
	}
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (!health.deathed) {
			if (!picked) {
				if (coll.gameObject.tag == "Weapon_Stick") {
					stickWeapon = coll.gameObject.GetComponent<StickWeaponScript> ();
					if (!stickWeapon.used) {
						if (!stickWeapon.isNunchakeChil) {
							// nhat weapon
							stickWeapon.SetupStick (ref rg2d, pointWeapon.position, layerName, ofPlayer, ref movement, ref this.Instance, ref swapColor, isLeft);
							picked = true;
							check_ ();
							return;
						} else {
							stickWeapon.stickParent.SetupStick (ref rg2d, pointWeapon.position, layerName, ofPlayer, ref movement, ref this.Instance, ref swapColor, isLeft);
							picked = true;
							check_ ();
							return;
						}
					} 
				}
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll)
	{
		if (!health.deathed) {
			if (!picked) {
				if (coll.gameObject.tag == "Weapon_Stick") {
					stickWeapon = coll.gameObject.GetComponent<StickWeaponScript> ();
					if (!stickWeapon.used) {
						if (!stickWeapon.isNunchakeChil) {
							// nhat weapon
							stickWeapon.SetupStick (ref rg2d, pointWeapon.position, layerName, ofPlayer, ref movement, ref this.Instance, ref swapColor, isLeft);
							picked = true;
							check_ ();
							return;
						} else {
							stickWeapon.stickParent.SetupStick (ref rg2d, pointWeapon.position, layerName, ofPlayer, ref movement, ref this.Instance, ref swapColor, isLeft);
							picked = true;
							check_ ();
							return;
						}
					}
				}
			}
		}
	}
	public void _freeWeapon ()
	{
		if (stickWeapon) {
			stickWeapon.resetStick ();
			resetPickUp ();
		}
	}
	public void resetPickUp ()
	{
//		if (duelPickUp.picked = false) {
//			duelPickUp.picked = true;
//			duelPickUp.StartCoroutine (duelPickUp.delay_resetPickUp ());
//		}
		if (this.picked)
			StartCoroutine (delay_resetPickUp ());
		print ("cai deo gi vay");
	}

	bool delayOff = false;

	IEnumerator delay_resetPickUp ()
	{
		yield return new WaitForSecondsRealtime (2f);
		picked = false;
		stickWeapon = null;
		delayOff = false;
	}

	void LateUpdate ()
	{
		if (picked) {
			if (!delayOff) {
				if (stickWeapon.transform.parent != this.transform) {
					StartCoroutine (delay_resetPickUp ());
					delayOff = true;
				}
			}
		}
	}

}
