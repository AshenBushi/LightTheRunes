using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class EnergyBarSet : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;

        public Slider energyBar;
        private int _currentLevel;
        private void Awake()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void Start()
        {
            _currentLevel = _saveData.save.currentLevel;

            switch (_currentLevel)
            {
                case 0:
                    energyBar.maxValue = 500;
                    energyBar.value = 500;
                    break;
                
                case 1:
                    energyBar.maxValue = 750;
                    energyBar.value = 750;
                    break;
                
                case 2:
                    energyBar.maxValue = 1000;
                    energyBar.value = 1000;
                    break;
            }
        }
    }
}
