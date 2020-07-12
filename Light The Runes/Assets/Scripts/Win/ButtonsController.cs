using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Win
{
    public class ButtonsController : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;

        public Button next;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        public void Home()
        {
            _functions.ToScene("MainMenu");
        }

        public void Restart()
        {
            if (_saveData.save.energy > 0)
            {
                _saveData.save.energy--;
                _functions.ToScene("gameplay");
            }
            else
            {
                _functions.EmptyEnergy();
            }
        }

        public void Next()
        {
            if (_saveData.save.energy > 0)
            {
                _saveData.save.energy--;
                _saveData.save.currentLevel += 1;
                _functions.ToScene("Gameplay");
            }
            else
            {
                _functions.EmptyEnergy();
            }
        }
    }
}
