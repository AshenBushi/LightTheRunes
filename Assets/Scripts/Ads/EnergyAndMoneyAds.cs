using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

public class EnergyAndMoneyAds : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private const string gameId = "3556619";
#elif UNITY_ANDROID
    private const string GameId = "3556618";
#endif
    
    private SessionData _sessionData;
    private ProgressData _progressData;
    
    private const string MyPlacementId = "rewardedVideo";
    
    private void Awake()
    {
        _sessionData = FindObjectOfType<SessionData>();
        _progressData = FindObjectOfType<ProgressData>();
        Advertisement.Initialize(GameId, false);
        Advertisement.AddListener (this);
    }

    public void OpenEnergyPanel()
    {
        gameObject.SetActive(true);
        _sessionData.sessionSave.pause = true;
    }
    
    public void Exit()
    {
        gameObject.SetActive(false);
        _sessionData.sessionSave.pause = false;
    }

    public void BuyEnergy()
    {
        if (_progressData.progressSave.money < 200 || _progressData.progressSave.energy >= 30) return;
        _progressData.progressSave.money -= 200;
        if(_progressData.progressSave.energy <= 20)
            _progressData.progressSave.energy += 10;
        else
            _progressData.progressSave.energy = 30;
        
        gameObject.SetActive(false);
    }
    
    public void ShowRewardedVideo()
    {
        if (_progressData.progressSave.energy >= 30) return;
        Advertisement.Show(MyPlacementId);
        Exit();
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == MyPlacementId)
        {}
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        _sessionData.sessionSave.pause = true;
    }

    public void OnUnityAdsDidFinish(string placementId, UnityEngine.Advertisements.ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished when _progressData.progressSave.energy <= 25:
                _progressData.progressSave.energy += 5;
                break;
            case ShowResult.Finished:
                _progressData.progressSave.energy = 30;
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
        _sessionData.sessionSave.pause = false;
    }
}
