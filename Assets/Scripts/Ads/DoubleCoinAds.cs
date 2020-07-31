using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads
{
    public class DoubleCoinAds : MonoBehaviour, IUnityAdsListener
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
        private const string MyPlacementId = "rewardedVideo";

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _progressData = FindObjectOfType<ProgressData>();
            Advertisement.Initialize(GameId, false);
            Advertisement.AddListener (this);
            doubleCoin.interactable = Advertisement.IsReady (MyPlacementId);
        }

        public void ShowRewardedVideo() 
        {
            Advertisement.Show(MyPlacementId);
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
                case ShowResult.Finished:
                    _progressData.progressSave.money += _sessionData.sessionSave.winMoney;
                    winMoney.text = (_sessionData.sessionSave.winMoney * 2).ToString();
                    doubleCoin.interactable = false;
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Failed:
                    break;
            }
            _sessionData.sessionSave.pause = false;
        }
    }
}
