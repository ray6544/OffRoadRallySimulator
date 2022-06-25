using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using System.Collections;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    protected static AdManager instance;
    public static AdManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = FindObjectOfType<AdManager>();
            if (instance != null)
            {
                return instance;
            }
            GameObject obj = new GameObject();
            obj.name = "Ads";
            instance = obj.AddComponent<AdManager>();
            return instance;
        }
    }
    public enum RewardedTypes { CoinsRush = 1, coinCash_grage = 2, coinCash_lvlSel = 3, Rewarededlos=4,DoubleReward=5};
    public RewardedTypes _RewardedTypes;
    public delegate void Ads();
    public static Ads  CoinsRush , coinCash_grage , coinCash_lvlSel , Rewarededlos , DoubleReward ;
    int RewardedType;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    //when publishing app 
    public bool publishingApp;

    public string testRewardId, testIntestellarId, testBannerId;

    [Header("Publishing")]
    public string RewardId, InterstitialId,  BannerId;
    [Header("-------------------------------------------")]
    public string GameId;
    public bool GameMode ;
    public string Interstitial;
    public string Interstitialvideo;
    [Header("-------------------------------------------")]
    [Header("Rate Us Section")]
    public int ClicksRateInGame;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();


    }
    // Start is called before the first frame update
    void Start()
    {

        Analytics.CustomEvent("start");
        
    }
    void Init()
    {

        if (publishingApp)
        {
            MobileAds.Initialize(initStatus => { });
        }
        else
        {
            List<string> deviceIds = new List<string>();
            deviceIds.Add("2077ef9a63d2b398840261c8221a0c9b");
            RequestConfiguration requestConfiguration = new RequestConfiguration
                .Builder()
                .SetTestDeviceIds(deviceIds)
                .build();
        }
        //REQUEST VIDEO
        this.RequestRewardBasedVideo();
        this.RequestBanner();
        RequestInterstitial();
        IniUnity();

    }

    private void RequestInterstitial()
    {
        string adUnitId = "";
        if (publishingApp)
        {
#if UNITY_ANDROID
            adUnitId = InterstitialId;
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = testIntestellarId;
#endif
        }
        // Initialize an InterstitialAd.
        this.interstitialAd = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);
    }
    
    private void RequestBanner()
    {
        string adUnitId = "";
        if (publishingApp)
        {
#if UNITY_ANDROID
            adUnitId = BannerId;
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = testBannerId;
#endif
        }
        AdSize adaptiveSize =new
                AdSize(320,50);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.TopRight);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);



    }
    
    private void RequestRewardBasedVideo()
    {
        string adUnitId = "";
        if (publishingApp)
        {
#if UNITY_ANDROID
            adUnitId = RewardId;
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = testRewardId;
#endif
        }
        //REWARDED ADS
        this.rewardedAd = new RewardedAd(adUnitId);
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);




    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Rewardedads();
    }
    public void LoadIntestellarAds()
    {
        if (PlayerPrefs.GetInt("Noads") == 0)
        {
            if (InternetConnectivity() == true)
            {
                if (interstitialAd.IsLoaded())
                {
                    interstitialAd.Show();

                }
                else
                {
                    ShowIntstitial();
                }
                RequestInterstitial();
            }
        }
    }
    
    public void DiplayRewardVideo( int index )
    {
        if (PlayerPrefs.GetInt("Noads") == 0)
        {
            if (InternetConnectivity() == true)
            {
                if (rewardedAd.IsLoaded())
                {
                    rewardedAd.Show();
                    RewardedType = index;

                }
                else
                {
                    ShowIntstitialVideo(index);
                }
                this.RequestRewardBasedVideo();
            }
        }
    }
    public void DisplayBanner()
    {
        if (PlayerPrefs.GetInt("Noads") == 0)
        {
            if (InternetConnectivity() == true)
            {
                bannerView.Show();
            }
        }
    }
    public void UndisplayBanner()
    {
        if (InternetConnectivity() == true)
        {
            bannerView.Hide();
        }
    }
    public bool IsRewardedVideoLoaded()
    {
        return rewardedAd.IsLoaded();
    }
    public bool IsIntestellarLoaded()
    {
        return interstitialAd.IsLoaded();
    }

    //=====================================Unity Ads Implementation=====================
    void IniUnity()
    {

        Advertisement.Initialize(GameId, GameMode);


    }
    public void ShowIntstitial()
    {
        if (PlayerPrefs.GetInt("Noads") == 0)
        {
            if (Advertisement.IsReady(Interstitial))
            {
                Advertisement.Show(Interstitial);
            }

        }
    }
    public void ShowIntstitialVideo(int index)
    {
        if (PlayerPrefs.GetInt("Noads") == 0)
        {
            if (Advertisement.IsReady(Interstitialvideo))
            {
                Advertisement.Show(Interstitialvideo);
                RewardedType = index;
            }

        }
    }


    public void HideUnityBanner()
    {
        Advertisement.Banner.Hide();
    }


    public void OnUnityAdsDidFinish(ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {

            Rewardedads();
    
            }
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
    public bool InternetConnectivity()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void Rewardedads()
    {
        switch ((RewardedTypes)RewardedType)
        {
            case RewardedTypes.CoinsRush:
                CoinsRush();
                break;
            case RewardedTypes.coinCash_grage:
                coinCash_grage();
                break;
            case RewardedTypes.coinCash_lvlSel:
                coinCash_lvlSel();
                break;
            case RewardedTypes.Rewarededlos:
                Rewarededlos();
                break;
            case RewardedTypes.DoubleReward:
                Rewarededlos();
                break;
        }
    }
}