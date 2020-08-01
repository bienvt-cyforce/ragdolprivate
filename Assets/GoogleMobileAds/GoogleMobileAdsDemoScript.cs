using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
//using UnityEngine.SceneManagement;
using Assets.Scripts.Singleton;


public class GoogleMobileAdsDemoScript : Singleton<GoogleMobileAdsDemoScript>
{
	//	private static GoogleMobileAdsDemoScript instance;
	//
	//	public static GoogleMobileAdsDemoScript Instance {
	//		get {
	//			return instance;
	//		}
	//	}
	private GoogleMobileAdsDemoScript ()
	{
	}

	private BannerView bannerView;
	private InterstitialAd interstitial;

	public string banner = "ca-app-pub-1667805177474426/9656769192";
	public string fullbanner = "ca-app-pub-1667805177474426/3610235595";

	void Awake ()
	{
		//RequestBanner ();
		//bannerView.Show ();
	}

	public void Start ()
	{		
//		if (instance == null) {
//			instance = this;
//		} else if (instance != this) {
//			Destroy (gameObject);
//		}
		//	DontDestroyOnLoad (gameObject);

		RequestInterstitial ();
//		RequestBanner ();
//		bannerView.Show ();
	}

	public void HideBanner ()
	{
		if (bannerView != null)
			bannerView.Hide ();
		Debug.Log ("Hide banner");
	}

	public void ShowBanner ()
	{
		if (bannerView != null) {
			bannerView.Show ();
		}
		Debug.Log ("Show banner");
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest ()
	{
		return new AdRequest.Builder ().Build ();
	}

	public void rateApp ()
	{
		print ("Open Url");
		Application.OpenURL ("https://itunes.apple.com/app/viewContentsUserReviews?id=1211175705");
	}

	public void RequestBanner ()
	{
		print ("fullbanner@" + banner);
		this.bannerView = new BannerView (banner, AdSize.SmartBanner, AdPosition.Bottom);
		//this.bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Top);
		// Register for ad events.
		this.bannerView.OnAdLoaded += this.HandleAdLoaded;
		this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView.OnAdOpening += this.HandleAdOpened;
		this.bannerView.OnAdClosed += this.HandleAdClosed;
		this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

		// Load a banner ad.
		this.bannerView.LoadAd (this.CreateAdRequest ());
		// Hide
	//	bannerView.Hide ();
	}

	public void RequestInterstitial ()
	{
		print ("fullbanner@" + fullbanner);
		// These ad units are configured to always serve test ads.
		// Create an interstitial.
		this.interstitial = new InterstitialAd (fullbanner);
		// Register for ad events.
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

		// Load an interstitial ad.
		this.interstitial.LoadAd (this.CreateAdRequest ());
	}

	public void ShowInterstitial ()
	{
		this.bannerView.Hide ();
		if (this.interstitial.IsLoaded ()) {
			this.interstitial.Show ();
			RequestInterstitial ();
		} else {
			UnityAds.Instance.revivePlayer = false;
			UnityAds.Instance.ShowAd ();
		}
	}

	#region Banner callback handlers

	public void HandleAdLoaded (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleAdLoaded event received");
	}

	public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print ("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleAdOpened event received");
	}

	public void HandleAdClosed (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleInterstitialLoaded event received");
	}

	public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print (
			"HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleInterstitialOpened event received");
		Time.timeScale = 0;
	}

	public void HandleInterstitialClosed (object sender, EventArgs args)
	{
		//Time.timeScale = 1;
		MonoBehaviour.print ("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleInterstitialLeftApplication event received");
	}

	#endregion

	#region Native express ad callback handlers

	public void HandleNativeExpressAdLoaded (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleNativeExpressAdAdLoaded event received");
	}

	public void HandleNativeExpresseAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print (
			"HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleNativeExpressAdOpened (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleNativeExpressAdAdOpened event received");
	}

	public void HandleNativeExpressAdClosed (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleNativeExpressAdAdClosed event received");
	}

	public void HandleNativeExpressAdLeftApplication (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleNativeExpressAdAdLeftApplication event received");
	}

	#endregion

	#region RewardBasedVideo callback handlers

	public void HandleRewardBasedVideoLoaded (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print (
			"HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleRewardBasedVideoClosed event received");
	}

	public void HandleRewardBasedVideoRewarded (object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print (
			"HandleRewardBasedVideoRewarded event received for " + amount.ToString () + " " + type);
	}

	public void HandleRewardBasedVideoLeftApplication (object sender, EventArgs args)
	{
		MonoBehaviour.print ("HandleRewardBasedVideoLeftApplication event received");
	}

	#endregion

}
