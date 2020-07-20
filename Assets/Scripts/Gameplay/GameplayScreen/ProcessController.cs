using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class ProcessController : MonoBehaviour
    {
        private SessionData _sessionData;
        private ProgressData _progressData;
        private Functions _functions;
        
        [SerializeField] private List<PadController> pads; 
        public Slider energyBar; 
        public TextMeshProUGUI energy;
        public GameObject gameplayScreen;
        public GameObject loseScreen;
        private float _value;
        private int _money;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _progressData = FindObjectOfType<ProgressData>();
            _functions = FindObjectOfType<Functions>();
            _sessionData.sessionSave.pause = false;
        }

        private void FixedUpdate()
        {
            if (!_sessionData.sessionSave.pause)
            {
                _value = energyBar.value - Time.deltaTime * 16.66f;
                energy.text = Mathf.RoundToInt(_value / 1).ToString();
                energyBar.value = _value;
            }

            if (_value >= 0) return;
            _sessionData.sessionSave.pause = true;
            gameplayScreen.SetActive(false);
            loseScreen.SetActive(true);

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
        private int _currentStars;
        private void WinMenu()
        {
            int whatLevel;
            if (_progressData.progressSave.currentLevel <= 20)
                whatLevel = 0;
            else if (_progressData.progressSave.currentLevel <= 41)
                whatLevel = 1;
            else
                whatLevel = 2;
            _money = 0;

            switch (_progressData.progressSave.currentLevel)
            {
                case 20:
                    _currentStars = 0;
                    break;
                case 41:
                    _currentStars = 0;
                    break;
                case 62:
                    _currentStars = 0;
                    break;
                default:
                    _currentStars = _progressData.progressSave.levelStar[_progressData.progressSave.currentLevel];
                    break;
            }
            
            if(_value >= energyBar.maxValue * 0.75)
            {
                for(var i = _currentStars; i < 3; i++)
                    _money += _reward[whatLevel];
                _progressData.progressSave.levelStar[_progressData.progressSave.currentLevel] = 3;
            }
            else
                if(_value >= energyBar.maxValue * 0.4)
                {
                    for(var i = _currentStars; i < 2; i++)
                        _money += _reward[whatLevel];
                    _progressData.progressSave.levelStar[_progressData.progressSave.currentLevel] = 2;
                }
                else
                {
                    for(var i = _currentStars; i < 1; i++)
                        _money += _reward[whatLevel];
                    _progressData.progressSave.levelStar[_progressData.progressSave.currentLevel] = 1;
                }
            
            _sessionData.sessionSave.winMoney = _money;
            _functions.ToScene("Win");
        }
    }
}
