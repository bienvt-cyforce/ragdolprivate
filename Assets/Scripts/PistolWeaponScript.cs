using UnityEngine;
using System.Collections;

public class PistolWeaponScript : MonoBehaviour
{
	public bool MAC10 = false;
	public StickWeaponScript Stick;
	public SpriteRenderer sprMuzz;
	float timer = 0;
	float timeShoot = 1f;
	public Transform posStart, posEnd,posDirectionAdForce;
	bool shooted = false;
	void Start(){
		sprMuzz.enabled = false;
		timeShoot = Random.Range (1.2f,1.6f);
	}
	void Update ()
	{
		if (this.Stick.used) {
			this.timer += Time.deltaTime;
			if (this.timer > this.timeShoot) {
				this._Shoot ();	
				this.timer = 0;
			}
		}
	}
	public void _Shoot ()
	{
		this.StartCoroutine (delay_muzzShoot ());
	}
	IEnumerator delay_muzzShoot(){
		Vector2 direc = this.posDirectionAdForce.position -  this.transform.position;
		this.Stick.addFoce_Pistol_01 (-1f*direc);
		yield return new WaitForSeconds (0.8f);
		this.Stick.addFoce_Pistol (direc);
		this.sprMuzz.enabled = true;
		yield return new WaitForEndOfFrame ();
		BulletPoolingManager.Instance.playSound ();
		yield return new WaitForSeconds (0.1f);
		Vector3 direction = this.posEnd.position - this.posStart.position;
		BulletPoolingManager.Instance._ChooseBullet (this.posStart.position, direction);
		if (MAC10) {
			StartCoroutine (delay_mac10 ());
		} else {
			this.sprMuzz.enabled = false;
		}
	}
	IEnumerator delay_mac10(){
		yield return new WaitForSeconds (0.1f);
		Vector3 direction = this.posEnd.position - this.posStart.position;
		BulletPoolingManager.Instance._ChooseBullet (this.posStart.position, direction);
		yield return new WaitForSeconds (0.1f);
		Vector3 direction2 = this.posEnd.position - this.posStart.position;
		BulletPoolingManager.Instance._ChooseBullet (this.posStart.position, direction2);
		yield return new WaitForSeconds (0.1f);
		this.sprMuzz.enabled = false;
	}
}
