using UnityEngine;
using System.Collections;

public class BodyScript : MonoBehaviour
{
	Rigidbody2D rg2d;
	public string nameTag;
	public float power;
	void Start ()
	{
		rg2d = this.GetComponent<Rigidbody2D> ();
	}
	void OnCollisionEnter2D (Collision2D coll)
	{
		
		if (coll.gameObject.tag == nameTag) {
			foreach (ContactPoint2D contact in coll.contacts) {
				if (contact.point != null) {
					add_Force (new Vector2 (coll.transform.position.x, coll.transform.position.y) - contact.point,1f);	
					return;
				}
			}
		}
	}
	void add_Force (Vector2 direction, float ratio)
	{
		direction.Normalize ();
		rg2d.AddForce (direction * power * ratio);
	}
}
