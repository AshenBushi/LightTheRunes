using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void FixedUpdate()
        {
            next.interactable = _saveData.save.energy >= 1;
        }

        public void Home()
        {
            _functions.ToScene("MainMenu");
        }

        public void Next()
        {
            _saveData.save.energy--;
            _functions.ToScene("Gameplay");
        }
    }
}
