/*
\n










*/

using UnityEngine;
using System;
using GoogleMobileAds.Api;


public class AdsControl : MonoBehaviour
{
	
	protected AdsControl ()
	{
	}

	private static AdsControl _instance;
    

	InterstitialAd interstitial;

	public string AdmobID_Android, AdmobID_IOS;

	public static AdsControl Instance { get { return _instance; } }

	void Awake ()
	{
		
		if (FindObjectsOfType (typeof(AdsControl)).Length > 1) {
			Destroy (gameObject);
			return;
		}
		
		_instance = this;
		MakeNewInterstial ();

		
		DontDestroyOnLoad (gameObject); //Already done by CBManager



	

	}


	public void HandleInterstialAdClosed (object sender, EventArgs args)
	{
	
		#if ADS_PLUGIN

		if (interstitial != null)
			interstitial.Destroy ();
		MakeNewInterstial ();
	
		#endif
		
	}

	void MakeNewInterstial ()
	{


#if UNITY_ANDROID
		interstitial = new InterstitialAd (AdmobID_Android);
#endif
#if UNITY_IPHONE
		interstitial = new InterstitialAd (AdmobID_IOS);
#endif
		interstitial.OnAdClosed += HandleInterstialAdClosed;
		AdRequest request = new AdRequest.Builder ().Build ();
		interstitial.LoadAd (request);


	}


	public void showAds ()
	{
		
	
		interstitial.Show ();
	
	

	}


	public bool GetRewardAvailable ()
	{
		bool avaiable = false;

		return avaiable;
	}

	public void ShowRewardVideo ()
	{
		
		
	}

	public void HideBannerAds ()
	{
	}

	public void ShowBannerAds ()
	{
	}


}

