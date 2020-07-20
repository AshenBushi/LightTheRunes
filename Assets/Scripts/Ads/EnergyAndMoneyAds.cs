using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Serialization;
using static UnityEngine.Monetization.Monetization;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class EnergyAndMoneyAds : MonoBehaviour
{
#if UNITY_IOS
    private const string gameId = "3556619";
#elif UNITY_ANDROID
    private const string GameId = "3556618";
#endif
    
    private SessionData _sessionData;
    private ProgressData _progressData;
    private Functions _functions;

    public GameObject energyAdPanel;
    
    private void Start()
    {
        if(isSupported)
            Initialize(GameId, false);
        _sessionData = FindObjectOfType<SessionData>();
        _progressData = FindObjectOfType<ProgressData>();
        _functions = FindObjectOfType<Functions>();
    }

    public void OpenEnergyPanel()
    {
        _sessionData.sessionSave.pause = true;
        energyAdPanel.SetActive(true);
    }
    
    public void Exit()
    {
        _sessionData.sessionSave.pause = false;
        energyAdPanel.SetActive(false);
    }
    
    public void AdEnergy()
    {
        if (_progressData.progressSave.energy >= 30) return;
        if (!IsReady("rewardedVideo")) return;
        _sessionData.sessionSave.pause = true;
        var options = new ShowAdCallbacks {finishCallback = EnergyShowResult};
        if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
            ad.Show(options);
    }

    private void EnergyShowResult(ShowResult result)
    {
        switch (result)
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
        energyAdPanel.SetActive(false);
    }

    public void BuyEnergy()
    {
        if (_progressData.progressSave.money < 200 || _progressData.progressSave.energy >= 30) return;
        _progressData.progressSave.money -= 200;
        if(_progressData.progressSave.energy <= 20)
            _progressData.progressSave.energy += 10;
        else
            _progressData.progressSave.energy = 30;
        
        energyAdPanel.SetActive(false);
    }
    
    public void AddMoney()
    {
        if (!IsReady("rewardedVideo")) return;
        _sessionData.sessionSave.pause = true;
        var options = new ShowAdCallbacks {finishCallback = MoneyShowResult};
        if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
            ad.Show(options);
    }

    private void MoneyShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                _progressData.progressSave.money += 100;
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }

        _sessionData.sessionSave.pause = false;
    }
}
