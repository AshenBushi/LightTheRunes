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

        public GameObject pausePanel;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _progressData = FindObjectOfType<ProgressData>();
            _functions = FindObjectOfType<Functions>();
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
    }
}
