using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class ButtonsController : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;

        public Button restart;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void FixedUpdate()
        {
            restart.interactable = _saveData.save.energy >= 1;
        }

        public void Home()
        {
            _functions.ToScene("MainMenu");
        }

        public void Restart()
        {
            _saveData.save.energy--;
            _functions.ToScene("Gameplay");
        }
    }
}
