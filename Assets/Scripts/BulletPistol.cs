using UnityEngine;
using System.Collections;

public class BulletPistol : MonoBehaviour
{
	public float moveSpeed = 0f;
	bool shooted = false;

	public void SetOnStart (Vector3 pos, Vector3 direction)
	{
		this.transform.position = pos;
		this.transform.rotation = Quaternion.AngleAxis (
			Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,
			new Vector3 (0, 0, 1));
		shooted = true;
	}

	void Update ()
	{
		if (shooted) {
			this.transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.tag == "platform") {
			this.gameObject.SetActive (false);
		}
	}

	void OnDisable ()
	{
		shooted = false;
	}
}
