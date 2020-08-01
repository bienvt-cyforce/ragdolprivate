using UnityEngine;
using System.Collections;

public class BulletPoolingManager : MonoBehaviour
{
	public static BulletPoolingManager Instance;
	public GameObject prefBullet;
	GameObject[] go;
	BulletPistol[] bulletPistol;
	public int numberBullet;
	public AudioSource ad;

	void Start ()
	{
		bool sound = (PlayerPrefs.GetInt ("SOUND") == 1) ? true : false;
		ad.mute = !sound;
		Instance = this;
		this.go = new GameObject[numberBullet];
		this.bulletPistol = new BulletPistol[numberBullet];
		for (int i = 0; i < numberBullet; i++) {
			this.go [i] = Instantiate (prefBullet) as GameObject;
			this.go [i].SetActive (false);
			this.bulletPistol [i] = go [i].GetComponent<BulletPistol> ();
		}
	}
	public void playSound(){
		ad.PlayOneShot (ad.clip);
	}
	public void _ChooseBullet (Vector3 pos, Vector3 direction)
	{
		for (int i = 0; i < numberBullet; i++) {
			if (!this.go [i].activeInHierarchy) {
				this.go [i].SetActive (true);
				bulletPistol [i].SetOnStart (pos, direction);
				return;
			}
		}
	}
}
