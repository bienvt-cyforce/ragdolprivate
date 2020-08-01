using UnityEngine;
using System.Collections;

public class StickWeaponScript : MonoBehaviour
{
	public Color32 colorNormal;
	public Color32 colorBlock;
	public SpriteRenderer[] sprs;
	public bool used = false;
	[HideInInspector]
	public FixedJoint2D fixedJoint2D;
	public bool ofPlayer = false;
	Rigidbody2D rg2d;
	float power;
	MovementScript movement;
	[HideInInspector]
	public LayerMask wp_maskOfPlayer, wp_maskOfEnemy, MaskLimit;
	public float frequencyFj, breakForce;
	string layerOfBoss;
	PickUpWeapon pickUp;
	SwapColorOnHit swapColorOnHit;
	public bool blockdame = false;
	[HideInInspector]
	public bool unIgnore = false;
	public bool attacked = false;
	public bool isDagger = false;
	public bool isBaton = false;
	public bool isNunchake = false;
	[Header ("if is Nunchake -> add")]
	public StickWeaponScript stickParent;
	public bool isNunchakeChil = false;
	[Header ("if is NunchakeChil -> add")]
	public StickWeaponScript stickChil;
	Transform transAxe;
	void Awake ()
	{
		frequencyFj = 16;
		breakForce = 3000f;
		power = BadLogic.powerOfStick;
		rg2d = this.GetComponent<Rigidbody2D> ();
		fixedJoint2D = this.GetComponent<FixedJoint2D> ();
		if (!isNunchakeChil) {
			fixedJoint2D.frequency = frequencyFj;
			fixedJoint2D.breakForce = breakForce;
			fixedJoint2D.enabled = false;
		}
		used = false;
		sprs = this.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < sprs.Length; i++) {
			if (sprs [i].tag != "BLOCKCOLOR") {
				sprs [i].color = colorNormal;
				sprs [i].sortingOrder = -2;
			} else {
				sprs [i].color = colorBlock;
				sprs [i].sortingOrder = -1;
			}
		}
		ofPlayer = false;
		blockdame = false;
		unIgnore = false;
		wp_maskOfEnemy = LayerMask.NameToLayer ("WeaponEnemy");
		wp_maskOfPlayer = LayerMask.NameToLayer ("WeaponPlayer");
		MaskLimit = LayerMask.NameToLayer ("LIMIT");
		rg2d.inertia = 0.1f;
		transAxe = this.transform.FindChild ("AxeCollider");
		if (transAxe) {
			transAxe.gameObject.tag = "Untagged";
		}
		rg2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	}
	public void SetupStick (ref Rigidbody2D rg2d, Vector3 pos, string layerName, bool stateOfP, ref MovementScript movementTemp, ref PickUpWeapon pickUpTemp, ref SwapColorOnHit swapTemp, bool isLeft)
	{
		if (isNunchake) {
			stickChil.SetupStick (ref rg2d, pos, layerName, stateOfP, ref movementTemp, ref pickUpTemp, ref swapTemp, isLeft);
		}
		swapColorOnHit = swapTemp;
		pickUp = pickUpTemp;
		ofPlayer = stateOfP;
		//this.gameObject.layer = LayerMask.NameToLayer (layerName);
		movement = movementTemp;
		if (!isNunchakeChil) {
			this.transform.position = pos;
			this.transform.parent = pickUp.transform;
			if (isLeft) {
				if (isDagger) {
					this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, 0));
				} else {
					if (isBaton) {
						this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
						this.transform.localScale = new Vector3 (-1, -1, 1);
					} else {
						this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, -90f));
					}
				}
			} else {
				if (isDagger) {
					this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
				} else {
					if (isBaton) {
						this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
						this.transform.localScale = new Vector3 (-1, 1, 1);
					} else {
						this.transform.localRotation = Quaternion.Euler (new Vector3 (0f, 0f, -90f));
					}
				}
			}
			if (fixedJoint2D) {
				fixedJoint2D.enabled = true;
				fixedJoint2D.connectedBody = rg2d;
			}
		}
		used = true;
		layerOfBoss = layerName;
		if (ofPlayer) {
			this.gameObject.layer = wp_maskOfPlayer;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer (layerName), wp_maskOfPlayer, true);
			Physics2D.IgnoreLayerCollision (wp_maskOfPlayer, MaskLimit, true);
			unIgnore = true;

		} else {
			this.gameObject.layer = wp_maskOfEnemy;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer (layerName), wp_maskOfEnemy, true);
			Physics2D.IgnoreLayerCollision (wp_maskOfEnemy, MaskLimit, true);
			unIgnore = true;
		}
		if (transAxe) {
			transAxe.gameObject.tag = "SPIKES";
			if (stateOfP) {
				transAxe.gameObject.layer = LayerMask.NameToLayer ("SAWofPLAYER");
			} else {
				transAxe.gameObject.layer = LayerMask.NameToLayer ("AXEofE");
			}
		}
	}

	public void addFoce_ (Vector2 direction)
	{
		if (swapColorOnHit) {
			swapColorOnHit.add_force (direction, 1f);
		}
		if (movement) {
			movement.add_force (direction);
		}
		direction.Normalize ();
		rg2d.AddForce (direction * power);
	}

	public void addFoce_Pistol (Vector2 direction)
	{
		if (swapColorOnHit) {
			swapColorOnHit.add_force (direction, 0.4f);
		}
		//		if (movement) {
		//			movement.add_force (direction);
		//		}
		direction.Normalize ();
		rg2d.AddForce (direction * power * 0.7f);
	}

	public void addFoce_Pistol_01 (Vector2 direction)
	{
		if (swapColorOnHit) {
			swapColorOnHit.add_force (direction, 0.2f);
		}
		//		if (movement) {
		//			movement.add_force (direction);
		//		}
		direction.Normalize ();
		rg2d.AddForce (direction * power * 0.7f);
	}

	public void resetStick ()
	{
		if (pickUp != null) {
			Vector3 direction = this.transform.position - this.pickUp.transform.position;
			addFoce_ (new Vector2 (direction.x, direction.y));
		}
		if (!isNunchakeChil) {
			used = false;
			fixedJoint2D.enabled = false;
			fixedJoint2D.connectedBody = null;
			fixedJoint2D.frequency = frequencyFj;
			fixedJoint2D.breakForce = breakForce;
			if (pickUp) {
				pickUp.resetPickUp ();
			}
		}
		if (ofPlayer) {
			if (layerOfBoss != "") {
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer (layerOfBoss), wp_maskOfPlayer, false);
				Physics2D.IgnoreLayerCollision (wp_maskOfPlayer, MaskLimit, false);
			}

		} else {
			if (layerOfBoss != "") {
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer (layerOfBoss), wp_maskOfEnemy, false);
				Physics2D.IgnoreLayerCollision (wp_maskOfEnemy, MaskLimit, false);
			}
		}
		if (!isNunchakeChil) {
			this.gameObject.layer = 0;
			this.transform.parent = null;
			swapColorOnHit = null;
		} 
		if (isNunchake) {
			stickChil.fixNunchakeChil ();
		}
		pickUp = null;
		if (transAxe) {
			transAxe.gameObject.layer = LayerMask.NameToLayer ("Axe");
			transAxe.gameObject.tag = "Untagged";
		}
		layerOfBoss = "";
	}

	public void fixNunchakeChil ()
	{
		used = false;
	}

	void Update ()
	{
		if (fixedJoint2D == null) {
			fixedJoint2D = this.gameObject.AddComponent<FixedJoint2D> ();
			resetStick ();
		}
		if (isNunchakeChil) {
			if (stickParent.used) {
				if (!used) {
					used = true;
				}
			} else {
				if (used) {
					used = false;
				}
			}
		}
	}

	int countBlockDame, countBlockDameLate;

	public void resetBlockDame ()
	{
		countBlockDame += 1;
		if (!blockdame) {
			countBlockDameLate = countBlockDame;
			blockdame = true;
			StartCoroutine (delay_BlockDame ());
		}
	}

	IEnumerator delay_BlockDame ()
	{
		yield return new WaitForSecondsRealtime (0.25f);
		if (countBlockDameLate == countBlockDame) {
			blockdame = false;
			countBlockDame = 0;
			countBlockDameLate = 0;
		} else {
			countBlockDameLate = countBlockDame;
			StartCoroutine (delay_BlockDame ());
		}
	}

	public void attack_Stick (Vector2 direction, Vector2 pos)
	{
		attacked = true;
		direction.Normalize ();
		rg2d.AddForceAtPosition (direction * power, pos);
		StartCoroutine (delay_attack_stick ());
	}

	IEnumerator delay_attack_stick ()
	{
		yield return new WaitForSecondsRealtime (1f);
		attacked = false;
	}

	SwapColorOnHit swapTemp;
	string tagTemp;
	//	void OnCollisionStay2D(Collision2D coll){
	//		if (!used) {
	//			tagTemp = null;
	//			tagTemp = coll.gameObject.tag;
	//			if (tagTemp == "BODY" || tagTemp == "ARM" || tagTemp == "HEAD") {
	//			}
	//		}
	//	}

	bool contactPlatform = false;

	void resetContactPlatform ()
	{
		contactPlatform = true;
		StartCoroutine (delay_resetContactPlatform ());
	}

	IEnumerator delay_resetContactPlatform ()
	{
		yield return new WaitForSecondsRealtime (1f);
		contactPlatform = false;
	}

	bool contactWeapon = false;

	void resetContactWeapon ()
	{
		contactWeapon = true;
		StartCoroutine (delay_resetContactWeapon ());
	}

	IEnumerator delay_resetContactWeapon ()
	{
		yield return new WaitForSecondsRealtime (1f);
		contactWeapon = false;
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (used) {
			if (coll.gameObject.tag == "platform") {
				if (!contactPlatform) {
					if (!attacked) {
						Vector2 direction;
						foreach (ContactPoint2D contact in coll.contacts) {
							direction = new Vector2 (this.transform.position.x, this.transform.position.y) - contact.point;
							if (movement) {
								movement.add_force_FixAI (direction);
							}
							resetContactPlatform ();
							return;
						}
					}
				}
			}
			if (coll.gameObject.tag == "Weapon_Stick") {
				if (!contactWeapon) {
					if (!attacked) {
						Vector2 direction;
						foreach (ContactPoint2D contact in coll.contacts) {
							direction = new Vector2 (this.transform.position.x, this.transform.position.y) - contact.point;
							attack_Stick (direction, contact.point);
							resetContactWeapon ();
							return;
						}
					}
				}
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll)
	{
		if (used) {
			//			if (coll.gameObject.tag == "platform") {
			//				if (!contactPlatform) {
			//					if (!attacked) {
			//						Vector2 direction;
			//						foreach (ContactPoint2D contact in coll.contacts) {
			//							direction = new Vector2 (this.transform.position.x, this.transform.position.y) - contact.point;
			//							if (movement) {
			//								movement.add_force_FixAI (direction);
			//							}
			//							resetContactPlatform ();
			//							return;
			//						}
			//					}
			//				}
			//			}
			if (coll.gameObject.tag == "Weapon_Stick") {
				if (!contactWeapon) {
					if (!attacked) {
						Vector2 direction;
						foreach (ContactPoint2D contact in coll.contacts) {
							direction = new Vector2 (this.transform.position.x, this.transform.position.y) - contact.point;
							attack_Stick (direction, contact.point);
							resetContactWeapon ();
							return;
						}
					}
				}
			}
		}
	}
}
