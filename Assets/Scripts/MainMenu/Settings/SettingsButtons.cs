using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Settings
{
    public class SettingsButtons : MonoBehaviour
    {
        private ProgressData _progressData;
        private SessionData _sessionData;
        private Functions _functions;

        public Image[] buttons;
        public Sprite buttonOn;
        public Sprite buttonOff;
        public GameObject settingsPanel;

        private void Start()
        {
            _progressData = FindObjectOfType<ProgressData>();
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void FixedUpdate()
        {
            buttons[0].sprite = _progressData.progressSave.sound ? buttonOn : buttonOff;
            buttons[1].sprite = _progressData.progressSave.music ? buttonOn : buttonOff;
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
    
        public void MusicChange()
        {
            _progressData.progressSave.music = !_progressData.progressSave.music;
        }
    
        public void SoundChange()
        {
            _progressData.progressSave.sound = !_progressData.progressSave.sound;
        }
    }
}
