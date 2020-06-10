using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using static UnityEngine.Monetization.Monetization;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class EnergyAndMoneyAds : MonoBehaviour
{
#if UNITY_IOS
    private const string gameId = "3556619";
#elif UNITY_ANDROID
    private const string GameId = "3556618";
#endif
    
    private SaveData _saveData;
    private Functions _functions;
    
    private void Start()
    {
        if(isSupported)
            Initialize(GameId, false);
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
    }

    public void AddEnergy()
    {
        if (!IsReady("rewardedVideo")) return;
        _saveData.save.pause = true;
        var options = new ShowAdCallbacks {finishCallback = EnergyShowResult};
        if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
            ad.Show(options);
    }

    private void EnergyShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished && _saveData.save.energy <= 25)
            _saveData.save.energy += 5;
        else 
        if (result == ShowResult.Finished)
            _saveData.save.energy = 30;
        else 
        if (result == ShowResult.Skipped) {}
        else
        if (result == ShowResult.Failed) {}
        _saveData.save.pause = false;
    }
    
    public void AddMoney()
    {
        if (!IsReady("rewardedVideo")) return;
        _saveData.save.pause = true;
        var options = new ShowAdCallbacks {finishCallback = MoneyShowResult};
        if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
            ad.Show(options);
    }

    private void MoneyShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
            _saveData.save.money += 100;
        else 
        if (result == ShowResult.Skipped) {}
        else
        if (result == ShowResult.Failed) {}
        _saveData.save.pause = false;
    }
}
