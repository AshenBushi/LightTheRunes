using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class PauseButtons : MonoBehaviour
    {
        private SessionData _sessionData;
        private ProgressData _progressData;
        private Functions _functions;

        public Image[] buttons;
        public Sprite buttonOn;
        public Sprite buttonOff;
        public GameObject pausePanel;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _progressData = FindObjectOfType<ProgressData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void FixedUpdate()
        {
            buttons[0].sprite = _progressData.progressSave.sound ? buttonOn : buttonOff;
            buttons[1].sprite = _progressData.progressSave.music ? buttonOn : buttonOff;
        }

        public void Home()
        {
            _functions.ToScene("MainMenu");
        }

        public void Restart()
        {
            if (_progressData.progressSave.energy > 0)
            {
                _progressData.progressSave.energy--;
                _functions.ToScene("gameplay");
            }
            else
            {
                pausePanel.SetActive(false);
                _functions.EmptyEnergy();
            }
        }

        public void Exit()
        {
            pausePanel.SetActive(false);
            _sessionData.sessionSave.pause = false;
        }

        public void Pause()
        {
            _sessionData.sessionSave.pause = true;
            pausePanel.SetActive(true);
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
