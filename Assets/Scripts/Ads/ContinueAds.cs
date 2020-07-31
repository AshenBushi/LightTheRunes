using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads
{
    public class ContinueAds : MonoBehaviour, IUnityAdsListener
    {

#if UNITY_IOS
        private const string gameId = "3556619";
#elif UNITY_ANDROID
        private const string GameId = "3556618";
#endif

        private SessionData _sessionData;

        public Slider energyBar;
        public GameObject gameplayScreen;
        public GameObject loseScreen;
        public Button continuePlay;
        private const string MyPlacementId = "rewardedVideo";

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            Advertisement.Initialize(GameId, false);
            Advertisement.AddListener (this);
        }

        public void ShowRewardedVideo() 
        {
            if(Advertisement.IsReady (MyPlacementId))
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
                    energyBar.value = energyBar.maxValue / 2;
                    Debug.Log(energyBar.value);
                    gameplayScreen.SetActive(true);
                    loseScreen.SetActive(false);
                    continuePlay.interactable = false;
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
