using UnityEngine;
using System.Collections;

public class FixLegContact : MonoBehaviour
{
	public SwapColorOnHit swapMain, swapMain_2, swapDual, swapDual_2;
	public Transform pointMain, pointDual;
	float minRange = 0.9f;
	float distance, timer;
	public bool checking = false;
	Transform transMainDirec, transDualDirec;
	GameObject pref;
	BoxCollider2D Box2dLeft, Box2dRight;
	void Awake ()
	{
		pref = Resources.Load<GameObject> ("PosFixLeg");
	}
	bool ignore = false;
	public void SetOnStart ()
	{
		GameObject go = Instantiate (pref);
		go.name = "PosFixLeg";
		go.transform.position = pointDual.position;
		go.transform.parent = pointMain;
		transMainDirec = go.transform;

		GameObject goDual = Instantiate (pref);
		goDual.name = "PosFixLegDual";
		goDual.transform.position = pointMain.position;
		goDual.transform.parent = pointDual;
		transDualDirec = goDual.transform;

//		Box2dLeft = this.swapMain.transform.parent.FindChild ("3Quad_Leg_Left").GetComponent<BoxCollider2D> ();
//		Box2dRight = this.swapDual.transform.parent.FindChild ("3Quad_Leg_Right").GetComponent<BoxCollider2D> ();
		swapMain_2 = this.swapMain.transform.parent.FindChild ("3Quad_Leg_Left (1)").GetComponent<SwapColorOnHit> ();
		swapDual_2 = this.swapDual.transform.parent.FindChild ("3Quad_Leg_Right (1)").GetComponent<SwapColorOnHit> ();
	}
	void Update ()
	{
		if (checking) {
			if (swapMain.enabled) {
				distance = Vector3.Distance (this.pointMain.position, this.pointDual.position);
				if (distance < minRange) {
					fixing ();
				}
			}
		}
	}
	private void fixing ()
	{
		if (!ignore) {
			//	Physics2D.IgnoreCollision (Box2dLeft, Box2dRight, true);
			Vector2 temp = new Vector2 (this.transMainDirec.position.x, this.transMainDirec.position.y) - new Vector2 (this.pointMain.position.x, this.pointMain.position.y);
			Vector2 temp_Dual = new Vector2 (this.transDualDirec.position.x, this.transDualDirec.position.y) - new Vector2 (this.pointDual.position.x, this.pointDual.position.y);
			this.swapMain.add_froce_fixStay (-temp, 1f);
			this.swapMain_2.add_froce_fixStay (-temp, 0.6f);
			this.swapDual.add_froce_fixStay (-temp_Dual, 1f);
		this.swapDual_2.add_froce_fixStay (-temp_Dual, 0.6f);
			ignore = true;
			this.StartCoroutine (this.delay_Ignore ());
		}
		//		if (!ignore) {
		//		}
	}
	IEnumerator delay_Ignore ()
	{
		yield return new WaitForSecondsRealtime (0.5f);
		ignore = false;
		//		yield return new WaitForSecondsRealtime (1f);
		//		ignore = false;
		//	Physics2D.IgnoreCollision (Box2dLeft, Box2dRight, false);
	}
}