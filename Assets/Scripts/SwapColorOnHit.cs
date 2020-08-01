using UnityEngine;
using System.Collections;

public class SwapColorOnHit : MonoBehaviour
{
	public Rigidbody2D rg2d;
	MovementScript movement;
	public SpriteRenderer[] spr;
	public Color colorHit;
	public Color colorHitA;
	public Color colorNormal;
	SetupScript setup;
	float timedelay;
	public string nameTag;
	public string nameTagAttack;
	string tagHit = "", layerHit = "";
	bool isHit = false;
	float power;
	bool isPlayer;
	bool shield = false;
	bool isHead = false;
	HealthScript healthScript;
	public bool blockdame = false;
	BoxCollider2D Box2d;
	public GameObject goTrigger;
	public string tagName = "";
	bool isBody = false;
	public bool isLeg = false;
	public bool isHand = false;
	public SwapColorOnHit LegLess;
	Vector2 directionLate;
	public float ratioAddForceHit = 0.16f;
	public float ratioAddForceDame = 0.16f;
	StickWeaponScript stickWeapon;
	PickUpWeapon pickUp;
	public bool death = false;
	string tagTriggerCheckAttack = "";
	Transform trans_pointAT;
	bool autoAttack;
	float ratioAttack = 100f;
	public bool isColorLocal = false;
	public Color ColorLocal;
	int orderLayerNormal = 1;
	public bool modeBoxer_Fix = false;
	bool modeBoxer = false;
	bool isVibration = false;
	bool callStart = false;
	bool bolbox = false;
	FixedJoint2D fixedJoint2D;
	Transform boxBoxCollider;
	bool fixWeapon = false;

	public void offCalls ()
	{
		callStart = false;
	}

	public void Awake ()
	{
		ratioAddForceHit = 0.6f;
		ratioAddForceDame = 7f;
		Box2d = this.GetComponent<BoxCollider2D> ();
		healthScript = this.GetComponentInParent<HealthScript> ();
		blockdame = false;
		isHead = false;
		shield = false;
		isHit = false;
		spr = GetComponentsInChildren<SpriteRenderer> ();
		setup = GetComponentInParent<SetupScript> ();
		rg2d = this.GetComponent<Rigidbody2D> ();
		movement = this.GetComponentInParent<MovementScript> ();
		power = movement.power;
		fixedJoint2D = this.GetComponent<FixedJoint2D> ();
		// Fix
		this.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
		tagName = setup.nameLayer_body;
		timedelay = setup.timedelay;

		if (nameTag != "") {
			this.gameObject.tag = nameTag;

			///	this.gameObject.layer = LayerMask.NameToLayer (setup.nameLayer_body);
			isHit = true;
			if (setup.nameLayer_body == "Player") {
				tagHit = "EnemyAttack";
			} else {
				tagHit = "PlayerAttack";
			}
		} else if (nameTagAttack != "") {
			isHit = false;
			try {
				this.gameObject.tag = setup.nameLayer_body + nameTagAttack;
			} catch {
				print (gameObject.name);
			}

			//	this.gameObject.layer = LayerMask.NameToLayer (setup.nameLayer_body + "Attack");
		}
		if (setup.nameLayer_body == "Player") {
			isPlayer = true;
		} else {
			isPlayer = false;
		}
		if (nameTag == "HEAD") {
			isHead = true;
		}
		blockdame_death = false;
		// Fix Error ok
		Box2d = null;
		isBody = (nameTag == "BODY") ? true : false;
		isHand = (nameTag == "ARM") ? true : false;
		if (isLeg) {
			pickUp = this.GetComponent<PickUpWeapon> ();
			nameTag = "LEG";
		}
		death = false;
		if (isPlayer) {
			tagTriggerCheckAttack = "SLOWTRIGGER";
		} else {
			tagTriggerCheckAttack = "SLOW_PLAYER";
		}
		if (!isHit) {
			trans_pointAT = transform.FindChild ("PointForce");
		}
		autoAttack = setup.autoAttack;
		ratioAttack = setup.ratioAttack;

		//		if (this.name == "3Quad_Leg_Right") {
		//			Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left").GetComponent<BoxCollider2D> (), true);
		//		}
		bolbox = GameObject.FindObjectOfType<GUIManager> ().modeBoxer;
		if (this.name == "HeadPlayer") {
			//Physics2D.IgnoreCollision (this.transform.parent.FindChild ("1Quad_Nect ").GetComponent<BoxCollider2D> (), this.GetComponent<CircleCollider2D> (), false);
		}
		if (this.name == "2Quad_Hand_Left (1)" || this.name == "2Quad_Hand_Right(1)") {
			nameTag = "HAND";
			fixedJoint2D.frequency = 9;
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left (1)").GetComponent<BoxCollider2D> (), false);
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right (1)").GetComponent<BoxCollider2D> (), false);
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("2Quad_NECT (2)_1_top").GetComponent<BoxCollider2D> (), false);
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), false);
		}
		if (this.name == "2Quad_Hand_Left") {
			GameObject go = Instantiate (Resources.Load<GameObject> ("Fixleg/BoxCollider_HandLeft")) as GameObject;
			boxBoxCollider = go.transform;
			boxBoxCollider.parent = this.transform;
			boxBoxCollider.localPosition = Vector3.zero;
			boxBoxCollider.localRotation = Quaternion.Euler (Vector3.zero);
			boxBoxCollider.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
			//if (boxBoxCollider) {
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), boxBoxCollider.GetComponent<Collider2D> (), true);
			//	Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild ("2Quad_Hand_Left (1)").GetComponent<BoxCollider2D> (), true);
			nameboxBoxColl = "2Quad_Hand_Left (1)";
			//	}
			fixedJoint2D.frequency = 4;
			//	this.GetComponent<FixedJoint2D> ().enableCollision = false;
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("2Quad_NECT (2)_1_top").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect ").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left").GetComponent<BoxCollider2D> (), false);
		}
		if (this.name == "2Quad_Hand_Right") {
			fixedJoint2D.frequency = 4;
			GameObject go = Instantiate (Resources.Load<GameObject> ("Fixleg/BoxCollider_HandRight")) as GameObject;
			boxBoxCollider = go.transform;
			boxBoxCollider.parent = this.transform;
			boxBoxCollider.localPosition = Vector3.zero;
			boxBoxCollider.localRotation = Quaternion.Euler (Vector3.zero);
			boxBoxCollider.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
			//if (boxBoxCollider) {
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), boxBoxCollider.GetComponent<Collider2D> (), true);
			//Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild ("2Quad_Hand_Right(1)").GetComponent<BoxCollider2D> (), true);
			nameboxBoxColl = "2Quad_Hand_Right(1)";
			//}
			//	this.GetComponent<FixedJoint2D> ().enableCollision = false;
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("2Quad_NECT (2)_1_top").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect ").GetComponent<BoxCollider2D> (), false);
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right").GetComponent<BoxCollider2D> (), false);
			///
		}
		if (this.name == "3Quad_Leg_Right") {
			fixedJoint2D.frequency = 8;
			Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), true);
			Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right (1)").GetComponent<BoxCollider2D> (), true);
			GameObject go = Instantiate (Resources.Load<GameObject> ("Fixleg/BoxCollider")) as GameObject;
			boxBoxCollider = go.transform;
			boxBoxCollider.parent = this.transform;
			boxBoxCollider.localPosition = Vector3.zero;
			boxBoxCollider.localRotation = Quaternion.Euler (Vector3.zero);
			boxBoxCollider.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
			//if (boxBoxCollider) {
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), boxBoxCollider.GetComponent<Collider2D> (), true);
			//	Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right (1)").GetComponent<BoxCollider2D> (), true);
			//	}
			nameboxBoxColl = "3Quad_Leg_Right (1)";
		} else if (this.name == "1Quad_Nect (1)_2_bot") {
			fixedJoint2D.frequency = 9;
		} else if (this.name == "2Quad_NECT (2)_1_top") {
			
			fixedJoint2D.frequency = 9;
			GameObject go = Instantiate (Resources.Load<GameObject> ("Fixleg/BoxCollider_Body")) as GameObject;
			boxBoxCollider = go.transform;
			boxBoxCollider.parent = this.transform;
			boxBoxCollider.localPosition = Vector3.zero;
			boxBoxCollider.localRotation = Quaternion.Euler (Vector3.zero);
			boxBoxCollider.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
			//if (boxBoxCollider) {
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), boxBoxCollider.GetComponent<Collider2D> (), true);
			//	Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), true);
			//	}
			nameboxBoxColl = "1Quad_Nect (1)_2_bot";

		} else if (this.name == "1Quad_Nect ") {
			if (fixedJoint2D != null) {
				fixedJoint2D.frequency = 18;
			}
		}
		if (this.name == "3Quad_Leg_Left") {
			GameObject go = Instantiate (Resources.Load<GameObject> ("Fixleg/BoxCollider")) as GameObject;
			boxBoxCollider = go.transform;
			boxBoxCollider.parent = this.transform;
			boxBoxCollider.localPosition = Vector3.zero;
			boxBoxCollider.localRotation = Quaternion.Euler (Vector3.zero);
			boxBoxCollider.gameObject.layer = LayerMask.NameToLayer (setup.fix_nameOfLayer);
			//if (boxBoxCollider) {
			//	print (this.transform.parent.name+"@@@@@@@@@deo vao day!!");
			//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), go.GetComponent<Collider2D> (), true);
			//	Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left (1)").GetComponent<BoxCollider2D> (), true);
			nameboxBoxColl = "3Quad_Leg_Left (1)";
			//}
			Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), true);
			Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Left (1)").GetComponent<BoxCollider2D> (), true);
			fixedJoint2D.frequency = 8;
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("3Quad_Leg_Right").GetComponent<BoxCollider2D> (), false);
			//Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect (1)_2_bot").GetComponent<BoxCollider2D> (), true);
			if (!bolbox) {
				FixLegContact temp = this.gameObject.AddComponent<FixLegContact> ();
				temp.swapMain = this;
				temp.swapDual = this.transform.parent.FindChild ("3Quad_Leg_Right").GetComponent<SwapColorOnHit> ();
				temp.pointMain = this.transform.FindChild ("PointForce");
				temp.pointDual = temp.swapDual.transform.FindChild ("PointForce");
				temp.SetOnStart ();
				temp.checking = true;
			}
		}
		if (bolbox) {
			ratioPowerAttack = 0.3f;
			power = power * 0.5f;
			ratioAddForceDame = 6f;
		} else {
			ratioPowerAttack = 0.26f;
		}
		this.rg2d.inertia = 0.31f;
		if (this.name == "3Quad_Leg_Left (1)" || this.name == "3Quad_Leg_Right (1)") {
			//	
			fixedJoint2D.frequency = 6;
			ratioAddForceHit = 0.2f;
			ratioAddForceDame = 4f;
			HingeJoint2D hingeJonit2d;
			hingeJonit2d = this.gameObject.AddMissingComponent<HingeJoint2D> ();
			hingeJonit2d.autoConfigureConnectedAnchor = true;
			hingeJonit2d.anchor = new Vector2 (0, 0.9678017f);
			string name;
			if (this.name == "3Quad_Leg_Left (1)") {
				name = "3Quad_Leg_Left";
				limit.min = -10;
				limit.max = 43;
			} else {
				name = "3Quad_Leg_Right";
				limit.min = -43;
				limit.max = 10;
			}
		
			hingeJonit2d.limits = limit;
			hingeJonit2d.enableCollision = false;
			hingeJonit2d.useLimits = true;
			hingeJonit2d.connectedBody = this.transform.parent.FindChild (name).GetComponent<Rigidbody2D> ();
			//this.GetComponent<FixedJoint2D> ().enableCollision = false;
		}
		if (this.name == "3Quad_Leg_Left" || this.name == "3Quad_Leg_Right") {
			//	this.GetComponent<FixedJoint2D> ().enableCollision = false;
		}
		isVibration = BadLogic.isVibration;
		fixWeapon = (pickUp) ? true : false;	
		try {
			this.GetComponent<FixedJoint2D> ().dampingRatio = 1f;
		} catch {
		}
	}

	JointAngleLimits2D limit;

	public void revivePlayer ()
	{
		blockdame = false;
		death = false;
		blockdame_death = false;
		shield = false;
	}
	string nameboxBoxColl;
	public void Start ()
	{
		
		if (!callStart) {
			if (boxBoxCollider) {
				//	Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), boxBoxCollider.GetComponent<Collider2D> (), true);
				Physics2D.IgnoreCollision (boxBoxCollider.GetComponent<Collider2D> (), this.transform.parent.FindChild (nameboxBoxColl).GetComponent<BoxCollider2D> (), true);
			}
			if (isHand) {
				if (bolbox) {
					//					Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("HeadPlayer").GetComponent<CircleCollider2D> (), true);
					//					Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), this.transform.parent.FindChild ("1Quad_Nect ").GetComponent<CircleCollider2D> (), true);
				}
			}
			if (!isColorLocal) {
				orderLayerNormal = 0;
				colorNormal = setup.colorNormal;
			} else {
				colorNormal = ColorLocal;
				orderLayerNormal = 1;
			}

			if (isHead) {
				orderLayerNormal += 1;
			}
			colorHit = setup.colorHit;
			colorHitA = setup.coloHitA;
			for (int i = 0; i < spr.Length; i++) {
				if (spr [i].tag != "BLOCKCOLOR") {
					spr [i].color = colorNormal;
					spr [i].sortingOrder = orderLayerNormal;
				}
			}
			modeBoxer = GUIManager.Instance.modeBoxer;
		}
		callStart = true;
	}

	public void changeColorOnHit ()
	{
		for (int i = 0; i < spr.Length; i++) {
			if (spr [i].tag != "BLOCKCOLOR") {
				spr [i].color = colorHit;
				spr [i].sortingOrder = orderLayerNormal + 1;
			}
		}
		StartCoroutine (delayHit ());
	}

	IEnumerator delayHit ()
	{
		SlowMotionScript.Instance.slowing = true;
		yield return new WaitForSecondsRealtime (timedelay / 1.5f);
		for (int i = 0; i < spr.Length; i++) {
			if (spr [i].tag != "BLOCKCOLOR") {
				spr [i].color = colorHitA;
			}
			//	spr [i].sortingOrder = 0;
		}
		yield return new WaitForSecondsRealtime (timedelay);
		for (int i = 0; i < spr.Length; i++) {
			if (spr [i].tag != "BLOCKCOLOR") {
				spr [i].color = colorNormal;
				spr [i].sortingOrder = orderLayerNormal;
			}
		}
	}

	public void add_froce_fixStay (Vector2 direction, float ratio)
	{
		direction.Normalize ();
		//	rg2d.velocity = Vector2.zero;
		rg2d.AddForce (direction * BadLogic.ranger * power * ratio);
	}

	public void add_force (Vector2 direction, float ratio)
	{
		//directionLate = direction;
		if (!delayAddForce) {
			direction.Normalize ();
			rg2d.AddForce (direction * BadLogic.ranger * power * ratio);
			if (isLeg) {
				if (!isHand) {
					LegLess.add_forceFix_Leg (direction, -ratio);
				} else {
					LegLess.add_forceFix_Leg (direction,ratio);
				}
			}
			delayAddForce = true;
			StartCoroutine (DelayAddForce ());
		}
	}

	public void add_forceFix_Leg (Vector2 direction, float ratio)
	{
		//fi		print ("@@adForce==============================))))))))))))");
		rg2d.AddForceAtPosition (direction * (BadLogic.ranger * ratio) * power * 0.3f, trans_pointAT.position);
	}

	IEnumerator DelayAddForce ()
	{
		//yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds(0.15f));
		//yield return new WaitForSeconds (Time.deltaTime);
		//yield return new WaitForEndOfFrame ();
		yield return new WaitForSecondsRealtime(0.1f);
		delayAddForce = false;
	}

	bool delayAddForce = false;

	public void add_froceOnDeath ()
	{
		death = true;
		if (pickUp) {
			if (pickUp.picked) {
				pickUp._freeWeapon ();
			}
		}
		if (goTrigger) {
			goTrigger.SetActive (false);
		}
		try {
			Vector2 direction = new Vector2 (this.transform.position.x, this.transform.position.y) - new Vector2 (this.transform.parent.position.x, this.transform.parent.position.y);

			if (isBody) {
				direction.Normalize ();
				direction = new Vector2 (-direction.x, direction.y);
			}
			if (isLeg) {
				direction.x = directionLate.x;
				direction.y = directionLate.y;
			}
			direction.Normalize ();
			rg2d.AddForce (direction * power * 2f);
		} catch {
		}
	}

	IEnumerator delayBox2d ()
	{
		yield return new WaitForSeconds (0.05f);
		if (Box2d) {
			Box2d.enabled = true;
		}
	}

	void OnCollisionStay2D (Collision2D coll)
	{
		//		Vector2 point = coll.contacts [0].point;
		//		if (!death) {
		//			if (!modeBoxer) {
		//				if (isPlayer) {
		//					if (!isHit) {
		//						if (!this.modeBoxer_Fix) {
		//							if (coll.gameObject.tag == "EnemyAttack") {
		//								tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
		//								if (!tempSwap.modeBoxer_Fix) {
		//									//foreach (ContactPoint2D contact in coll.contacts) {
		//									point = coll.contacts [0].point;
		//									if (tempSwap.enabled) {
		//										if (!tempSwap.blockdame) {
		//											if (point != null) {
		//												tempSwap.add_force (point - new Vector2 (this.transform.position.x, this.transform.position.y), ratioAddForceHit);
		//												HitSmokeManager.Instance.playEffect (point);
		//												//movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
		//												add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceHit);
		//												return;
		//											}
		//										}
		//									}
		//									//}
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//			if (coll.gameObject.tag == tagHit) {
		//				tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
		//				if (isHit) {
		//					//foreach (ContactPoint2D contact in coll.contacts) {
		//					//	point = coll.contacts[0].point;
		//					if (tempSwap.enabled) {
		//						if (!tempSwap.modeBoxer_Fix) {
		//							if (this.gameObject.tag != "LEG") {
		//								if (!tempSwap.blockdame) {
		//									if (point != null) {
		//										tempSwap.add_force (new Vector2 (tempSwap.transform.position.x, tempSwap.transform.position.y) - point, ratioAddForceDame);
		//										//movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
		//										add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
		//										HitSmokeManager.Instance.playEffect (point);
		//										//										if (!blockdame) {
		//										//											if (!shield) {
		//										//												changeColorOnHit ();
		//										//												if (this.gameObject.tag == "HEAD") {
		//										//													this.changeHealth (2);
		//										//												} else {
		//										//													this.changeHealth (1);
		//										//												}
		//										//												HiAManScript.Instance.play (point);
		//										//												HitCircleManager.Instance.playHit (point);
		//										//												StartCoroutine (waitEndFrame (point, nameTag, coll.gameObject.GetComponent<TypeAttack> ().typeAttack.ToString ()));
		//										//												StartCoroutine (delayShield ());
		//										////												this.BlockDame_ ();
		//										////												tempSwap.BlockDame_ ();
		//										//												return;
		//										//											}
		//										//										}
		//										return;
		//									}
		//								}
		//							}
		//						}
		//					}
		//					//}
		//				}
		//				if (coll.gameObject.tag == "Weapon_Stick") {
		//					stickWeapon = coll.gameObject.GetComponent<StickWeaponScript> ();
		//					if (stickWeapon.used) {
		//						if (isPlayer) {
		//							if (!stickWeapon.ofPlayer) {
		//								//point = coll.contacts[0].point;
		//								//foreach (ContactPoint2D contact in coll.contacts) {
		//								if (point != null) {
		//									movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
		//									add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, 1f);
		//									stickWeapon.addFoce_ (point - new Vector2 (this.transform.position.x, this.transform.position.y));
		//									HitSmokeManager.Instance.playEffect (point);
		//									return;
		//								}
		//							}
		//						} else {
		//							if (stickWeapon.ofPlayer) {
		//								//point = coll.contacts[0].point;
		//								//foreach (ContactPoint2D contact in coll.contacts) {
		//								if (point != null) {
		//									movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
		//									add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, 1f);
		//									stickWeapon.addFoce_ (point - new Vector2 (this.transform.position.x, this.transform.position.y));
		//									HitSmokeManager.Instance.playEffect (point);
		//									return;
		//								}
		//								//}
		//							}
		//						}
		//					}
		//				}
		//			}
		//			if (this.tag == coll.gameObject.tag) {
		//				if (this.gameObject.layer == coll.gameObject.layer) {
		//					tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
		//					if (tempSwap.enabled) {
		//						if (!tempSwap.death) {
		//							if (point != null) {
		//								if (!isHit) {
		//									add_forceFix_Leg (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceHit * 2f);
		//									tempSwap.add_forceFix_Leg (point - new Vector2 (this.transform.position.x, this.transform.position.y), ratioAddForceHit * 2f);
		//									return;
		//								} else {
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//			if (!bolbox) {
		//				try {
		//					tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
		//					if (tempSwap) {
		//						if (!tempSwap.movement.addForceFixAI) {
		//							if (isPlayer == tempSwap.isPlayer) {
		//								if (coll.transform.parent.GetInstanceID () != this.transform.parent.GetInstanceID ()) {
		//									//point = coll.contacts[0].point;
		//									//foreach (ContactPoint2D contact in coll.contacts) {
		//									tempSwap.movement.add_force_FixAI (point - new Vector2 (this.transform.position.x, this.transform.position.y));
		//									return;
		//									//}
		//								}
		//							}
		//						}
		//					}
		//				} catch {
		//
		//				}
		//			}
		//		}
	}

	SwapColorOnHit tempSwap;

	void OnCollisionEnter2D (Collision2D coll)
	{
		Vector2 point = coll.contacts [0].point;
		tempSwap = null;
		if (!blockdame) {
			// Spikes
			if (coll.gameObject.tag == "SPIKES") {
				//foreach (ContactPoint2D contact in coll.contacts) {
				//	point = coll.contacts[0].point;
				if (point != null) {
					movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
					add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
					if (!shield) {
						changeColorOnHit ();
						if (this.gameObject.tag == "HEAD") {
							if (!isPlayer) {
								this.changeHealthNotSlow (2);
							} else {
								this.changeHealth (2);
							}
						} else {
							if (!isPlayer) {
								this.changeHealthNotSlow (1);
							} else {
								this.changeHealth (1);
							}
						}
						HiAManScript.Instance.play (point);
						HitCircleManager.Instance.playHit (point);
						string typeAT = coll.gameObject.GetComponent<TypeAttack> ().typeAttack.ToString ();
						if (typeAT == "BULLET") {
							coll.gameObject.SetActive (false);
						}
						StartCoroutine (waitEndFrame (point, nameTag, typeAT));
						StartCoroutine (delayShield ());
						this.BlockDame_ ();
						return;
					}
				}
				//}
			}

			//Weapon
			if (coll.gameObject.tag == "Weapon_Stick") {
				stickWeapon = coll.gameObject.GetComponent<StickWeaponScript> ();
				if (stickWeapon.used) {
					if (!stickWeapon.blockdame) {
						if (isPlayer) {
							if (!stickWeapon.ofPlayer) {
								//	point = coll.contacts[0].point;
								//foreach (ContactPoint2D contact in coll.contacts) {
								if (point != null) {
									if (!isHit) {
										HitSmokeManager.Instance.playEffect (point);
									} else {
										if (!shield) {
											changeColorOnHit ();
											this.changeHealth (1);
											StartCoroutine (delayShield ());
											StartCoroutine (waitEndFrame (point, nameTag, coll.gameObject.GetComponent<TypeAttack> ().typeAttack.ToString ()));
											HiAManScript.Instance.play (point);
											HitCircleManager.Instance.playHit (point);
										}
									}
									movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
									add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
									stickWeapon.addFoce_ (point - new Vector2 (this.transform.position.x, this.transform.position.y));
									this.BlockDame_ ();
									stickWeapon.resetBlockDame ();
									return;
								}
								//}
							}
						} else {
							if (stickWeapon.ofPlayer) {
								//point = coll.contacts[0].point;
								//foreach (ContactPoint2D contact in coll.contacts) {
								if (point != null) {
									if (!isHit) {
										HitSmokeManager.Instance.playEffect (point);
									} else {
										if (!shield) {
											changeColorOnHit ();
											this.changeHealth (1);
											StartCoroutine (delayShield ());
											StartCoroutine (waitEndFrame (point, nameTag, coll.gameObject.GetComponent<TypeAttack> ().typeAttack.ToString ()));
											HiAManScript.Instance.play (point);
											HitCircleManager.Instance.playHit (point);
										}
									}
									movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
									add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
									stickWeapon.addFoce_ (point - new Vector2 (this.transform.position.x, this.transform.position.y));
									this.BlockDame_ ();
									stickWeapon.resetBlockDame ();
									return;
								}
								//}
							}
						}
					}
				}
			}
			// body + Leg + Hand
			if (isPlayer) {
				if (!modeBoxer) {
					if (!isHit) {
						if (!this.modeBoxer_Fix) {
							if (coll.gameObject.tag == "EnemyAttack") {
								tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
								if (!tempSwap.modeBoxer_Fix) {
									//foreach (ContactPoint2D contact in coll.contacts) {
									//	point = coll.contacts[0].point;
									if (tempSwap.enabled) {
										if (!tempSwap.blockdame) {
											if (point != null) {
												tempSwap.add_force (new Vector2 (tempSwap.transform.position.x, tempSwap.transform.position.y) - point, ratioAddForceHit);
												tempSwap.movement.add_force (new Vector2 (tempSwap.transform.position.x, tempSwap.transform.position.y) - point);
												HitSmokeManager.Instance.playEffect (point);
												movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
												add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceHit);
												StartCoroutine (delayShield ());
												this.BlockDame_ ();
												tempSwap.BlockDame_ ();
												return;
											}
										}
									}
									//}
								}
							}
						}
					}
				}
			}
			if (coll.gameObject.tag == tagHit) {
				tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
				//foreach (ContactPoint2D contact in coll.contacts) {
				//point = coll.contacts[0].point;
				if (isHit) {
					if (tempSwap.enabled) {
						if (!tempSwap.modeBoxer_Fix) {
							if (this.gameObject.tag != "LEG") {
								if (point != null) {
									if (!tempSwap.blockdame) {
										//	print ("cai meo gi vay");
										movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
										add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
										tempSwap.movement.add_force (point - new Vector2 (this.transform.position.x, this.transform.position.y));
										tempSwap.add_force (point - new Vector2 (this.transform.position.x, this.transform.position.y), ratioAddForceDame);
										if (!shield) {
											changeColorOnHit ();
											if (this.gameObject.tag == "HEAD") {
												this.changeHealth (2);
											} else {
												this.changeHealth (1);
											}
											HiAManScript.Instance.play (point);
											HitCircleManager.Instance.playHit (point);

											StartCoroutine (waitEndFrame (point, nameTag, coll.gameObject.GetComponent<TypeAttack> ().typeAttack.ToString ()));
											StartCoroutine (delayShield ());
											this.BlockDame_ ();
											tempSwap.BlockDame_ ();
											return;
										}
									}
								}
							}
						}
					}
				}
			}
			if (isHead) {
				if (isPlayer) {
					if (!modeBoxer_Fix) {
						if (coll.gameObject.tag == "HEAD") {
							tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
							//foreach (ContactPoint2D contact in coll.contacts) {
							if (tempSwap.enabled) {
								if (!tempSwap.blockdame) {
									if (point != null) {
										tempSwap.add_force (point - new Vector2 (this.transform.position.x, this.transform.position.y), ratioAddForceDame);
										if (!tempSwap.shield) {
											tempSwap.changeColorOnHit ();
											tempSwap.changeHealth (2);
											StartCoroutine (tempSwap.delayShield ());
										}
										movement.add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
										add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point, ratioAddForceDame);
										if (!shield) {
											changeColorOnHit ();
											this.changeHealth (2);
											StartCoroutine (waitEndFrame (point, nameTag, "BOUNCE"));
											HiAManScript.Instance.play (point);
											HitCircleManager.Instance.playHit (point);
											StartCoroutine (delayShield ());
										}
										this.BlockDame_ ();
										tempSwap.BlockDame_ ();
										return;
									}
								}
							}
						}
						//}
					}
				}
			}

			if (isHit) {
				if (!modeBoxer_Fix) {
					if (!isHead) {
						if (coll.gameObject.layer.ToString () != this.gameObject.layer.ToString ()) {
							try {
								tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
								if (this.tagName != tempSwap.tagName) {
									if (tempSwap.isHit) {
										if (!tempSwap.modeBoxer_Fix) {
											//	foreach (ContactPoint2D contact in coll.contacts) {
											if (tempSwap.enabled) {
												if (!tempSwap.blockdame) {
													if (point != null) {
														float ratio = 0.4f;
														if (tempSwap.isHead) {
															if (!isHead) {
																if (!shield) {
																	changeColorOnHit ();
																	this.changeHealth (1);
																	HiAManScript.Instance.play (point);
																	HitCircleManager.Instance.playHit (point);
																	StartCoroutine (waitEndFrame (point, nameTag, "BUTT"));
																	StartCoroutine (delayShield ());
																	ratio = ratioAddForceDame;
																}
															}
														}
														movement.add_force (new Vector2 (this.transform.parent.position.x, this.transform.parent.position.y) - point);
														//add_force (new Vector2 (this.transform.position.x, this.transform.position.y) - point);
														tempSwap.movement.add_force (new Vector2 (tempSwap.transform.parent.position.x, tempSwap.transform.parent.position.y) - point);
														this.BlockDame_ ();
														tempSwap.BlockDame_ ();
														return;
													}
												}
											}
										}
										//}
									}
								}
							} catch {
							}
						} 
					}
				}
			}

			if (!death) {
				try {
					tempSwap = coll.gameObject.GetComponent<SwapColorOnHit> ();
					if (!tempSwap.movement.addForceFixAI) {
						if (tempSwap) {
							if (coll.transform.parent.GetInstanceID () != this.transform.parent.GetInstanceID ()) {
								if (isPlayer == tempSwap.isPlayer) {
									//foreach (ContactPoint2D contact in coll.contacts) {
									//print ("cai meo gi vay");
									tempSwap.movement.add_force_FixAI (point - new Vector2 (this.transform.position.x, this.transform.position.y));
									return;
									//}
								}
							}
						}
					}
				} catch {

				}
			}
		}

	}

	bool blockdame_death = false;

	public void BlockDame_Death ()
	{
		blockdame_death = true;
		blockdame = true;
	}

	public void BlockDame_ ()
	{
		if (!blockdame) {
			blockdame = true;
			StartCoroutine (delayBlockDame ());
		}
	}

	IEnumerator delayBlockDame ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.25f));
		if (!blockdame_death) {
			blockdame = false;
		}
	}

	public void changeHealthNotSlow (int value)
	{
		healthScript.changeHealth (value);
	}

	public void changeHealth (int value)
	{
		int temp = 1;
		SlowMotionScript.Instance._SlowOnHit ();
		if (!isPlayer) {
			if (BadLogic.boosterStengh) {
				temp = 2;
			}
		}
		healthScript.changeHealth (value * temp);
		SoundManager.Instance.playClipDame ();
		if (isPlayer) {
			if (isVibration) {
				Handheld.Vibrate ();
				print ("RUNGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
			}
		}
	}

	IEnumerator waitEndFrame (Vector3 pos, string nameHit, string useSkill)
	{
		yield return new WaitForEndOfFrame ();
		SkillTextManager.Instance.playEffect (pos, nameHit + " " + useSkill);
	}

	IEnumerator delayShield ()
	{
		shield = true;
		yield return new WaitForEndOfFrame ();
		shield = false;
	}

	public void Destroythisgo (float time)
	{
		Destroy (this.gameObject, time);
	}

	bool attacked = false;

	bool attacking ()
	{
		if (ratioAttack == 100f) {
			return true;
		} else {
			float f = Random.Range (1, 100f);
			if (f < ratioAttack) {
				return true;
			} else {
				return false;
			}
		}
	}

	float ratioPowerAttack = 0.18f;

	public void addForce_Attack (Vector2 direction)
	{
		if (!delayAddForce) {
			if (attacking ()) {
				Vector2 pos;
				pos.x = trans_pointAT.position.x;
				pos.y = trans_pointAT.position.y;
				SlowMotionScript.Instance._slowMotion ();
				rg2d.AddForceAtPosition (direction * power * ratioPowerAttack, pos);
				StartCoroutine (delayAttack ());
			}
		}
	}

	IEnumerator delayAttack ()
	{
		yield return new WaitForSecondsRealtime (1f);
		attacked = false;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (autoAttack) {
			if (!isHit) {
				if (!blockdame) {
					if (!attacked) {
						if (coll.gameObject.tag == tagTriggerCheckAttack) {
							SwapColorOnHit tempS = coll.GetComponentInParent<SwapColorOnHit> ();
							if (GUIManager.Instance.modeBoxer) {
								if (tempS.isHit) {
									if (!tempS.blockdame) {
										attacked = true;
										Vector2 direction = new Vector2 (coll.transform.position.x, coll.transform.position.y) - new Vector2 (this.transform.position.x, this.transform.position.y);
										print (coll.transform.parent.name + coll.transform.name + " @attack by @" + this.gameObject.name);
										addForce_Attack (direction);
									}
								}
							} else {
								if (!tempS.blockdame) {
									attacked = true;
									Vector2 direction = new Vector2 (coll.transform.position.x, coll.transform.position.y) - new Vector2 (this.transform.position.x, this.transform.position.y);
									//print (coll.transform.parent.name + coll.transform.name + " @attack by @" + this.gameObject.name);
									addForce_Attack (direction);
								}
							}
						}
					}
				}
			}
		}
	}

	bool blockAddforceFixLeg = false;

	void OnTriggerStay2D (Collider2D coll)
	{
		if (autoAttack) {
			if (!isHit) {
				if (!blockdame) {
					if (!attacked) {
						if (coll.gameObject.tag == tagTriggerCheckAttack) {
							SwapColorOnHit tempS = coll.GetComponentInParent<SwapColorOnHit> ();
							if (GUIManager.Instance.modeBoxer) {
								if (tempS.isHit) {
									if (!tempS.blockdame) {
										attacked = true;
										Vector2 direction = new Vector2 (coll.transform.position.x, coll.transform.position.y) - new Vector2 (this.transform.position.x, this.transform.position.y);
										print (coll.transform.parent.name + coll.transform.name + " @attack by @" + this.gameObject.name);
										addForce_Attack (direction);
									}
								}
							} else {
								if (!tempS.blockdame) {
									attacked = true;
									Vector2 direction = new Vector2 (coll.transform.position.x, coll.transform.position.y) - new Vector2 (this.transform.position.x, this.transform.position.y);
									//print (coll.transform.parent.name + coll.transform.name + " @attack by @" + this.gameObject.name);
									addForce_Attack (direction);
								}
							}
						}
					}
				}
			}
		}
		if (!bolbox) {
			if (fixWeapon) {
				if (pickUp.picked) {
					if (coll.gameObject.tag == tagHit) {
						if (!blockAddforceFixLeg) {
							Vector2 direction = new Vector2 (this.transform.position.x, this.transform.position.y) - new Vector2 (coll.transform.position.x, coll.transform.position.y);
							this.movement.add_force (direction);
							blockAddforceFixLeg = true;
							this.StartCoroutine (delay_blockAddforceFixLeg ());
							return;
						}
					}
				}
			}
		}
	}

	IEnumerator delay_blockAddforceFixLeg ()
	{
		yield return new WaitForSecondsRealtime (0.5f);
		blockAddforceFixLeg = false;
	}
}
