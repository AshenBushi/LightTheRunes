using System;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;
using ShowResult = UnityEngine.Monetization.ShowResult;

namespace Gameplay
{
    public class ContinueAds : MonoBehaviour
    {

#if UNITY_IOS
        private const string gameId = "3556619";
#elif UNITY_ANDROID
        private const string GameId = "3556618";
#endif
    
        private SessionData _sessionData;
        private Functions _functions;
        
        public Slider energyBar;
        public GameObject gameplayScreen;
        public GameObject loseScreen;
        public Button continuePlay;
    
        private void Start()
        {
            if(Monetization.isSupported) Monetization.Initialize(GameId, false);
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();
        }

        public void Continue()
        {
            if (!Monetization.IsReady("rewardedVideo")) return;
            _sessionData.sessionSave.pause = true;
            var options = new ShowAdCallbacks {finishCallback = ContinueShowResult};
            if (Monetization.GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
                ad.Show(options);
        }

        private void ContinueShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    energyBar.value = energyBar.maxValue / 2;
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
