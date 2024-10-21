using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "5713495";
    [SerializeField] string _iOSGameId = "5713494";
    [SerializeField] bool _testMode = false;
    private string _gameId;
    private AdsManager adsMN;
    private AdsBanner bannerAds;

    void Awake()
    {
        InitializeAds();
        adsMN = GameObject.Find("AdsMN").GetComponent<AdsManager>();
        bannerAds = GameObject.Find("BannerAdsMN").GetComponent<AdsBanner>();
    }
    public void InitializeAds()
    {
    #if UNITY_ANDROID
        _gameId = _androidGameId;
    #elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
    #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported) Advertisement.Initialize(_gameId, _testMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        bannerAds.LoadBanner();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
}
