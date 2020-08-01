using UnityEngine;
using System.Collections;

public class TextCoinManager : MonoBehaviour {
	public static TextCoinManager Instance;
	public GameObject pref;
	GameObject[] Gos;
	public int maxNum;
	void Start(){
		Instance = this;
		Gos = new GameObject[maxNum];
		for (int i = 0; i < maxNum; i++) {
			Gos[i] = Instantiate (pref) as GameObject;
			Gos[i].name += i;
			Gos[i].transform.parent = this.transform;
			Gos[i].SetActive (false);
		}
	}
	public void playEffect(Vector3 pos,string txt){
		for (int i = 0; i < maxNum; i++) {
			if (Gos [i]) {
				if (!Gos [i].activeInHierarchy) {
					Gos [i].transform.position = new Vector2 (pos.x, pos.y - (float)i*0.5f);
					Gos [i].SetActive (true);
					Gos [i].GetComponent<TitleSkillEffect> ().SetText (txt);
					return;
				}
			}
		}
	}
}
