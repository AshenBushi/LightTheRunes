using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnergyAndMoneyController : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;
    
    public TextMeshProUGUI energy;
    public TextMeshProUGUI money;
    public TextMeshProUGUI secondsTimer;
    public TextMeshProUGUI minuteTimer;
    public GameObject fake;
    private int _energy, _money;

    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
        
        _energy = _saveData.save.energy;
        _money = _saveData.save.money;
        energy.text = _energy.ToString();
        money.text = _money.ToString();
    }
    
    private void FixedUpdate()
    {
        money.text = _saveData.save.money.ToString();
        energy.text = _energy.ToString();
        EnergyTimer();
    }
    
    private void SaveData()
    {
        _saveData.save.energy = _energy;
        _saveData.save.energySave = _energySave;
        _saveData.save.energyTimer = _energyTimer;
        _saveData.SetDateTime(DateTime.UtcNow);
    }

    private void LoadData()
    {
        _energyTimer = _saveData.save.energyTimer;
        if (_energyTimer < 0)
            _energyTimer = 0;
        _energy = _saveData.save.energy;
        _energySave = _saveData.save.energySave;
    }

    private int _energyTimer, _energyToRespond, _energySave;
    
    private void EnergyTimer()
    {
        LoadData();
        _energyToRespond = 30 - _energy;
        
        if (_energyToRespond <= 0)
        {
            _saveData.save.energySave = 0;
            _saveData.save.energyTimer = 0;
            _saveData.SetDateTime(DateTime.UtcNow);
            fake.SetActive(true);
            secondsTimer.text = 0.ToString();
            minuteTimer.text = "0";
            return;
        }
        
        _energyTimer += 120 * (_energyToRespond - _energySave);
        _energySave = _energyToRespond;

        var secondsPassed = (int) (DateTime.UtcNow - (_saveData.GetDateTime(DateTime.UtcNow))).TotalSeconds;

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
