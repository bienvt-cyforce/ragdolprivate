using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {

	private static AdsController instance;
	public static AdsController Instance{
		get{ 
			return instance;
		}
	}
	private BannerView bannerView;
	private InterstitialAd interstitial;
	//private RewardBasedVideoAd rewardBasedVideo;

	public string banner = "ca-app-pub-1667805177474426/9656769192";
	public string fullbanner = "ca-app-pub-1667805177474426/3610235595";


	public void Awake() {		
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else if (instance != this) {
			Destroy (gameObject);
		}
		//RequestInterstitial ();
	//	RequestBanner ();
	//	bannerView.Show ();

		// Get singleton reward based video ad reference.
//		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
//
//		// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
//		this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
//		this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
//		this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
//		this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
//		this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
//		this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
//		this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

	}	

	public void Start(){
		RequestBanner ();
		RequestInterstitial ();
	}

	public void HideBanner(){
		if (this.bannerView != null) {
			this.bannerView.Hide ();
		}
	}		

	public void ShowBanner(){
		if (this.bannerView != null) {
			this.bannerView.Show ();
		}
	}

//	public void RequestReward(){
//		this.RequestRewardBasedVideo();
//	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest() {
		return new AdRequest.Builder().Build();
	}
	public void RequestBanner() {
		if (this.bannerView == null) {
			this.bannerView = new BannerView (banner, AdSize.SmartBanner, AdPosition.Bottom);
			// Register for ad events.
			this.bannerView.OnAdLoaded += this.HandleAdLoaded;
			this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
			this.bannerView.OnAdOpening += this.HandleAdOpened;
			this.bannerView.OnAdClosed += this.HandleAdClosed;
			this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

			// Load a banner ad.
			this.bannerView.LoadAd (this.CreateAdRequest ());
			this.HideBanner ();
		}
	}
	public void RequestInterstitial() {
		this.interstitial = new InterstitialAd(fullbanner);
		// Register for ad events.
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		this.interstitial.LoadAd(this.CreateAdRequest());
	}


	public void ShowInterstitial() {
		if (this.interstitial != null) {
			if (this.interstitial.IsLoaded ()) {
				this.interstitial.Show ();
				RequestInterstitial ();
			}
		}
	}

//	private void RequestRewardBasedVideo() {
//		#if UNITY_EDITOR
//		string adUnitId = "unused";
//		#elif UNITY_ANDROID
//		string adUnitId = "INSERT_AD_UNIT_HERE";
//		#elif UNITY_IPHONE
//		string adUnitId = "INSERT_AD_UNIT_HERE";
//		#else
//		string adUnitId = "unexpected_platform";
//		#endif
//
//		this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
//	}

//	private void ShowRewardBasedVideo() {
//		if (this.rewardBasedVideo.IsLoaded()) {
//			this.rewardBasedVideo.Show();
//		} else {
//			MonoBehaviour.print("Reward based video ad is not ready yet");
//		}
//	}

	#region Banner callback handlers

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received");
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialOpened event received");
		Time.timeScale = 0;
	}

	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}

	#endregion

	#region Native express ad callback handlers

	public void HandleNativeExpressAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
	}

	public void HandleNativeExpresseAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleNativeExpressAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
	}

	public void HandleNativeExpressAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
	}

	public void HandleNativeExpressAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
	}

	#endregion

	#region RewardBasedVideo callback handlers

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
			"HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
	}
	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}
	#endregion
//	public void UnityAds(){
//		if (Advertisement.IsReady ()) {
//			Advertisement.Show ("", new ShowOptions (){ resultCallback = HandleAdResult });
//		} else {
//			//GameController.Instance.GameOver ();
//		}
//	}
//
//	private void HandleAdResult(ShowResult result){
//		switch (result) {
//		case ShowResult.Finished:
//			//GameController.Instance.RespawnPlayer ();
//			break;
//		case ShowResult.Skipped:
//			//GameController.Instance.RespawnPlayer ();
//			break;
//		case ShowResult.Failed:
//		//	GameController.Instance.GameOver ();
//			break;
//		}
//	}

}
