using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnergyAndMoneyController : MonoBehaviour
{
    private ProgressData _progressData;
    private Functions _functions;
    
    public TextMeshProUGUI energy;
    public TextMeshProUGUI money;
    public TextMeshProUGUI secondsTimer;
    public TextMeshProUGUI minuteTimer;
    public GameObject fake;
    private int _energy, _money;

    private void Start()
    {
        _progressData = FindObjectOfType<ProgressData>();
        _functions = FindObjectOfType<Functions>();
        
        _energy = _progressData.progressSave.energy;
        _money = _progressData.progressSave.money;
        energy.text = _energy.ToString();
        money.text = _money.ToString();
    }
    
    private void FixedUpdate()
    {
        money.text = _progressData.progressSave.money.ToString();
        energy.text = _energy.ToString();
        EnergyTimer();
    }
    
    private void SaveData()
    {
        _progressData.progressSave.energy = _energy;
        _progressData.progressSave.energySave = _energySave;
        _progressData.progressSave.energyTimer = _energyTimer;
        _progressData.SetDateTime(DateTime.UtcNow);
    }

    private void LoadData()
    {
        _energyTimer = _progressData.progressSave.energyTimer;
        if (_energyTimer < 0)
            _energyTimer = 0;
        _energy = _progressData.progressSave.energy;
        _energySave = _progressData.progressSave.energySave;
    }

    private int _energyTimer, _energyToRespond, _energySave;
    
    private void EnergyTimer()
    {
        LoadData();
        _energyToRespond = 30 - _energy;
        
        if (_energyToRespond <= 0)
        {
            _progressData.progressSave.energySave = 0;
            _progressData.progressSave.energyTimer = 0;
            _progressData.SetDateTime(DateTime.UtcNow);
            fake.SetActive(true);
            secondsTimer.text = 0.ToString();
            minuteTimer.text = "0";
            return;
        }
        
        _energyTimer += 120 * (_energyToRespond - _energySave);
        _energySave = _energyToRespond;

        var secondsPassed = (int) (DateTime.UtcNow - (_progressData.GetDateTime(DateTime.UtcNow))).TotalSeconds;

        if (_energyTimer >= 0)
        {
            _energyTimer -= secondsPassed;
            
            if (_energyTimer <= 120 * (_energyToRespond - 1))
            {
                _energy++;
                _energySave--;
            }
        }

        if (_energyTimer > 59)
        {
            secondsTimer.text = (_energyTimer % 60).ToString();
            minuteTimer.text = ((_energyTimer / 60) % 2).ToString();
        }
        else
        {
            secondsTimer.text = _energyTimer.ToString();
            minuteTimer.text = "0";
        }

        fake.SetActive((_energyTimer % 60) < 10);
        SaveData();
    }
}
