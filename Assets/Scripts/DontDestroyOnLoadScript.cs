using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadScript : MonoBehaviour {
	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
