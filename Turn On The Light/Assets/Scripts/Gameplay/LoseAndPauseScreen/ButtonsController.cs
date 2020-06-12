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

        public Image[] buttons;
        public Sprite buttonOn;
        public Sprite buttonOff;
        public Button restart;
        public GameObject pausePanel;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void FixedUpdate()
        {
            restart.interactable = _saveData.save.energy >= 1;
            buttons[0].sprite = _saveData.save.sound ? buttonOn : buttonOff;
            buttons[1].sprite = _saveData.save.music ? buttonOn : buttonOff;
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

        public void Exit()
        {
            pausePanel.SetActive(false);
            _saveData.save.pause = false;
        }

        public void Pause()
        {
            pausePanel.SetActive(true);
            _saveData.save.pause = true;
        }
        
        public void MusicChange()
        {
            _saveData.save.music = !_saveData.save.music;
        }
    
        public void SoundChange()
        {
            _saveData.save.sound = !_saveData.save.sound;
        }
    }
}
