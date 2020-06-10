using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;
using static UnityEngine.Monetization.Monetization;
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
    
        private SaveData _saveData;
        private Functions _functions;
        
        public Slider energyBar;
        public Transform mainCamera;
    
        private void Start()
        {
            if(isSupported)
                Initialize(GameId, false);
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        public void Continue()
        {
            if (!IsReady("rewardedVideo")) return;
            _saveData.save.pause = true;
            var options = new ShowAdCallbacks {finishCallback = ContinueShowResult};
            if (GetPlacementContent("rewardedVideo") is ShowAdPlacementContent ad) 
                ad.Show(options);
        }

        private void ContinueShowResult(ShowResult result)
        {
            if (result == ShowResult.Finished)
            {
                energyBar.value = energyBar.maxValue / 2;
                mainCamera.position = new Vector3(0f, 0f, -10f);
            }
            else if (result == ShowResult.Skipped) {}
            else if (result == ShowResult.Failed) {}
            _saveData.save.pause = false;
        }
    }
}
