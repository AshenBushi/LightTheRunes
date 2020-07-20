using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Win
{
    public class ButtonsController : MonoBehaviour
    {
        private ProgressData _progressData;
        private Functions _functions;

        public Button next;

        private void Start()
        {
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
                _functions.EmptyEnergy();
            }
        }

        public void Next()
        {
            if (_progressData.progressSave.energy > 0)
            {
                _progressData.progressSave.energy--;
                _progressData.progressSave.currentLevel += 1;
                _functions.ToScene("Gameplay");
            }
            else
            {
                _functions.EmptyEnergy();
            }
        }
    }
}
