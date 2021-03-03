using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string playStoreID = "3714603";
    private string appStoreID = "3714602";

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargerPlayStore;
    public bool isTestAd;

    public int rewardervideoCoins = 100;
    public TextMeshProUGUI coinsText;

    private static string type = "none"; //coins or respawn
    public CheckZone checkZone;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
    }

    private void Update()
    {
        Debug.Log(type);
    }

    private void InitializeAdvertisement()
    {
        if (isTargerPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd);
            return;
        }
        Advertisement.Initialize(appStoreID, isTestAd);
    }

    public void PlayInterstialAd(string _type)
    {
        if (!Advertisement.IsReady(interstitialAd))
            return;
        Advertisement.Show(interstitialAd);
        type = _type;
    }

    public void PlayRewarderVideoAd(string _type)
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
            return;
        type = _type;
        Advertisement.Show(rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Played1");
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Played2");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Played3");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Played");
        //throw new System.NotImplementedException();
        switch (showResult)
        {
            case ShowResult.Failed:
                Advertisement.RemoveListener(this);
                break;
            case ShowResult.Skipped:
                Advertisement.RemoveListener(this);
                break;
            case ShowResult.Finished:
                if(placementId == rewardedVideoAd)
                {
                    if (type == "coins")
                    {
                        Debug.Log("Reward the player with coins");
                        AddCoins();
                        UpdateUI();
                        UIManager.ButtonAdRewardCoins.SetActive(false);
                        UIManager.ButtonAdRewardRespawn.SetActive(false);  
                    }
                    else if (type == "respawn")
                    {
                        Debug.Log("Reward the player with respawn");
                        Respawn();
                        UIManager.ButtonAdRewardRespawn.SetActive(false);
                        //UIManager.ButtonAdRewardCoins.SetActive(false);
                    }
                    
                    int watched = PlayerPrefs.GetInt("Ad");
                    watched++;
                    PlayerPrefs.SetInt("Ad", watched);
                }
                if (placementId == interstitialAd)
                {
                    Debug.Log("Finished interstitial ad");
                }
                //Advertisement.RemoveListener(this);
                break;
        }
    }

    public void UpdateUI()
    {
        coinsText.text = SaveData.GetCoins().ToString();
    }

    private void AddCoins()
    {
        SaveData.AddCoins(rewardervideoCoins);
    }

    private void Respawn()
    {
        if(checkZone != null)
            checkZone.OnRespawn();
    }

    private void OnDisable()
    {
        Advertisement.RemoveListener(this);
    }
}
