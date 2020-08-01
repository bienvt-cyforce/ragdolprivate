using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelWatchVideo : MonoBehaviour
{
	public static PanelWatchVideo Instance;
	public Text[] txtNumber = new Text[3];
	public int[] number = new int[3];
	float timer = 0;
	public float durationFrame;
	bool play = false;
	int count = 0;
//	public GameObject btnWatchVideo;
	public Image imgBlockOnRandomCoin;
	public WatchVideoGetCoin wvds;
//	public Text txtcancel;
	void Start ()
	{
		Instance = this;
	}

	void OnEnable ()
	{
	//	txtcancel.text = "CANCEL";
	//	btnWatchVideo.SetActive (true);
	//	txtcancel.transform.parent.gameObject.SetActive (true);
		imgBlockOnRandomCoin.enabled = false;
	}

	void Update ()
	{
//		if (Input.GetKeyDown (KeyCode.J)) {
//			this.StartAnim ();
//		}
		if (!play)
			return;
		timer += Time.unscaledDeltaTime;
		if (timer >= durationFrame) {
			this._PlayRanDom ();
			timer = 0;
		}
	}

	void  _PlayRanDom ()
	{
		//
		count++;
		int i = Random.Range (1, 10);
		if (i < 9) {
			this.number [0] = Random.Range (0, 3);
		} else {
			if (i < 9) {
				this.number [0] = Random.Range (3, 6);
			} else {
				this.number [0] = Random.Range (6, 10);
			}
		}
		number [1] = Random.Range (1, 10);
		number [2] = Random.Range (1, 10);
		this.showTextNumber ();
	}

	string s = "";

	void showTextNumber ()
	{
		if (count > 20) {
			play = false;
		}
		for (int i = 0; i < 3; i++) {
			txtNumber [i].text = "" + number [i];
			if (!play) {
				s += number [i].ToString ();
			} 
		}
		if (!play) {
			//txtcancel.transform.parent.gameObject.SetActive (true);
//			CoinManager.coin += int.Parse (s);
//			CoinManager.UpCoin ();
//			if (GUIManager.Instance != null) {
//				GUIManager.Instance.UpdateTextCoin ();
//			} else {
//				SelectMapManager.Instance.SetTextCoin ();
//				EffectAddCoin.Instance.showCoin (int.Parse (s));
//			}
			wvds.randomCoin(int.Parse (s));
			imgBlockOnRandomCoin.enabled = false;
	//		txtcancel.text = "DONE!";
		}
	}
	public void StartAnim ()
	{
	//	txtcancel.transform.parent.gameObject.SetActive (false);
		play = true;
		count = 0;
		s = "";
		imgBlockOnRandomCoin.enabled = true;
	}
}
