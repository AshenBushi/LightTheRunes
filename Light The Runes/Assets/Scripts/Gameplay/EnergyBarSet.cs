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
            int i;

            if (_saveData.save.currentLevel <= 21)
                i = 0;
            else if (_saveData.save.currentLevel <= 42)
                i = 1;
            else
                i = 2;
            
            switch (i)
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
