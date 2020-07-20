using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;
using static UnityEngine.Monetization.Monetization;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class DoubleCoinAds : MonoBehaviour
{
#if UNITY_IOS
        private const string gameId = "3556619";
#elif UNITY_ANDROID
    private const string GameId = "3556618";
#endif
    
    private SessionData _sessionData;
    private ProgressData _progressData;

    public TextMeshProUGUI winMoney;
    public Button doubleCoin;

    private void Start()
    {
        if(isSupported)
            Initialize(GameId, false);
        _sessionData = FindObjectOfType<SessionData>();
        _progressData = FindObjectOfType<ProgressData>();
    }

    public void DoubleCoin()
    {
        if (!IsReady("rewardedVideo")) return;
        _sessionData.sessionSave.pause = true;
        var options = new ShowAdCallbacks {finishCallback = DoubleCoinShowResult};
        if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
            ad.Show(options);
    }

    private void DoubleCoinShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            _progressData.progressSave.money += _sessionData.sessionSave.winMoney;
            winMoney.text = (_sessionData.sessionSave.winMoney * 2).ToString();
            doubleCoin.interactable = false;
        }
        else if (result == ShowResult.Skipped) {}
        else if (result == ShowResult.Failed) {}
        _sessionData.sessionSave.pause = false;
    }
}
