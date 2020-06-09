using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class ProcessController : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;
        
        [SerializeField] private List<PadController> pads; 
        public Slider energyBar; 
        public TextMeshProUGUI energy;
        private float _value;
        private int _money;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void FixedUpdate()
        {
            _value = energyBar.value - Time.deltaTime * 16.66f;
            energy.text = Mathf.RoundToInt(_value / 1).ToString();
            energyBar.value = _value;
        }
        public void CheckForWin()
        {
            if (pads.Any(padController => padController.isTurn == false))
            {
                return;
            }

            WinMenu();
        }

        private void WinMenu()
        {
            _money = 0;
            if(_value >= energyBar.maxValue * 0.75)
            {
                for(var i = 0; i < 3; i++)
                    _money += 10;
            }
            else
                if(_value >= energyBar.maxValue * 0.4)
                {
                    for(var i = 0; i < 2; i++)
                        _money += 10;
                }
                else
                {
                    for(var i = 0; i < 1; i++)
                        _money += 10;
                }

            _saveData.save.winMoney = _money;
            _functions.ToScene("win");
        }

    }
}
