using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Settings
{
    public class SettingsButtons : MonoBehaviour
    {
        private ProgressData _progressData;
        private SessionData _sessionData;
        private Functions _functions;
        
        public GameObject settingsPanel;

        private void Start()
        {
            _progressData = FindObjectOfType<ProgressData>();
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();
        }

        public void Exit()
        {
            settingsPanel.SetActive(false);
            _sessionData.sessionSave.pause = false;
        }

        public void Settings()
        {
            settingsPanel.SetActive(true);
            _sessionData.sessionSave.pause = true;
        }
    }
}
