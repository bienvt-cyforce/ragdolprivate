using UnityEngine;
using System.Collections;

public class AutoCheckOut : MonoBehaviour
{
	Transform headPlayer;
	Transform _parent;

	void Start ()
	{
		_parent = this.transform.parent;
		headPlayer = this.transform.parent.FindChild ("HeadPlayer");
	}

	public void _findParentHead ()
	{
		//print (gameObject.name);
		this.transform.parent = headPlayer;
		this.transform.localPosition = Vector3.zero;
	}

	public void findParentPlayer ()
	{
		this.transform.parent = _parent;
//		_parent.transform.position = Vector3.zero;
		this.transform.localPosition = new Vector3 (0, -2.71f, 0);
	}
}
