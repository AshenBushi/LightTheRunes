using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class EnergyBarSet : MonoBehaviour
    {
        private ProgressData _progressData;
        private SessionData _sessionData;
        
        public Slider energyBar;
        public Animator animator;
        private AudioSource _lowHpClock;
        private int _currentLevel;
        private bool _checkHp;

        private void Awake()
        {
            _progressData = FindObjectOfType<ProgressData>();
            _sessionData = FindObjectOfType<SessionData>();

            _lowHpClock = GetComponent<AudioSource>();
        }

        private void Start()
        {
            int i;
            _checkHp = true;
            if (_progressData.progressSave.currentLevel < 21)
                i = 0;
            else if (_progressData.progressSave.currentLevel < 42)
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

        private void Update()
        {
            if (energyBar.value < energyBar.maxValue * 0.33 && !_checkHp)
            {
                _lowHpClock.Play();
                animator.SetBool("LowHp", true);
                _checkHp = true;
            }

            if (energyBar.value > energyBar.maxValue * 0.33 && _checkHp)
            {
                _lowHpClock.Stop();
                animator.SetBool("LowHp", false);
                _checkHp = false;
            }
            _lowHpClock.mute = !_progressData.progressSave.sound || _sessionData.sessionSave.pause;
        }
    }
}
