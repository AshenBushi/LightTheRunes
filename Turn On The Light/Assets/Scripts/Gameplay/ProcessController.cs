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
        public Transform mainCamera;
        private float _value;
        private int _money, _currentStars;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
            _saveData.save.pause = false;
        }

        private void FixedUpdate()
        {
            if (!_saveData.save.pause)
            {
                _value = energyBar.value - Time.deltaTime * 16.66f;
                energy.text = Mathf.RoundToInt(_value / 1).ToString();
                energyBar.value = _value;
            }

            if (_value >= 0) return;
            _saveData.save.pause = true;
            mainCamera.position = new Vector3(21.6f, 0f, -10f);
        }
        public void CheckForWin()
        {
            if (pads.Any(padController => padController.isTurn == false))
            {
                return;
            }

            WinMenu();
        }

        private readonly int[] _reward = {5, 10, 15};
        
        private void WinMenu()
        {
            _money = 0;
            if(_value >= energyBar.maxValue * 0.75)
            {
                for(var i = 0; i < 3; i++)
                    _money += _reward[_saveData.save.currentLevel];
                _currentStars = 3;
            }
            else
                if(_value >= energyBar.maxValue * 0.4)
                {
                    for(var i = 0; i < 2; i++)
                        _money += _reward[_saveData.save.currentLevel];
                    _currentStars = 2;
                }
                else
                {
                    for(var i = 0; i < 1; i++)
                        _money += _reward[_saveData.save.currentLevel];
                    _currentStars = 1;
                }

            _saveData.save.levelStar[_saveData.save.currentLevel] = _currentStars;
            _saveData.save.winMoney = _money;
            _functions.ToScene("Win");
        }
    }
}
