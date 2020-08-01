using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Multiplayer : MonoBehaviour
{
	public static Multiplayer Instance;
	public GameObject GUIMultiplayer;
	public GameObject GUIVersus, panelBlock;

	void Start ()
	{
		Instance = this;
		GUIMultiplayer.SetActive (false);
		GUIVersus.SetActive (false);
		panelBlock.SetActive (false);
	}

	public void clickMultiplayer ()
	{
		GUIMultiplayer.SetActive (true);
	}

	public void clickVersus ()
	{
		GUIVersus.SetActive (true);
	}

	public void back_versus ()
	{
		GUIVersus.SetActive (false);
	}

	public void back_multiplayer ()
	{
		GUIMultiplayer.SetActive (false);
	}

	public void click_classic ()
	{
		//	GUIMultiplayer.SetActive (false);
		panelBlock.SetActive (true);
		StartCoroutine (load_2Player ());
	}

	public void click_Boxer ()
	{
		//	GUIMultiplayer.SetActive (false);
		panelBlock.SetActive (true);
		StartCoroutine (load_2PlayerBoxer ());
	}

	IEnumerator load_2Player ()
	{
		if (MusicManager.Instance != null) {
			MusicManager.Instance.stopMusic ();
		}
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		//		if (!firstTimePlayGame) {
		//			//GoogleMobileAdsDemoScript.Instance.ShowInterstitial ();
		//		}
		SceneManager.LoadScene ("2 Player");
	}

	IEnumerator load_2PlayerBoxer ()
	{
		if (MusicManager.Instance != null) {
			MusicManager.Instance.stopMusic ();
		}
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.5f));
		//		if (!firstTimePlayGame) {
		//			//GoogleMobileAdsDemoScript.Instance.ShowInterstitial ();
		//		}
		SceneManager.LoadScene ("2PlayerBoxer");
	}
}
