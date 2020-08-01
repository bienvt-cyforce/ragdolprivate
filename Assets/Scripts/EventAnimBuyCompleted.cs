using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventAnimBuyCompleted : MonoBehaviour
{
	public Text txtTitle;
	public Animator anim;
	public GameObject panel;

	void Start ()
	{
		this.gameObject.SetActive (false);
	}

	public void setText (string text)
	{
		this.gameObject.SetActive (true);
		anim.Rebind ();
		anim.enabled = true;
		txtTitle.text = text + " +1";
	}

	public void disableAnim ()
	{
		anim.enabled = false;
		panel.SetActive (false);
	}

	public void setTextCoin (string text)
	{
		this.gameObject.SetActive (true);
		anim.Rebind ();
		anim.enabled = true;
		txtTitle.text = text;
	}
}
