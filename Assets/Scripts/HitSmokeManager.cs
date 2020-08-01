using UnityEngine;
using System.Collections;

public class HitSmokeManager : MonoBehaviour
{
	public static HitSmokeManager Instance;
	public CreateObjectPooling ob;
	Vector3 posLate;

	void Start ()
	{
		Instance = this;
		posLate = Vector3.zero;
	}

	public void playEffect (Vector3 pos)
	{
		if (Vector3.Distance (pos, posLate) > 1f) {
			ob._ChooseEffect (pos);
			SoundManager.Instance.playClipHit ();
			posLate = pos;
		}
	}
}
