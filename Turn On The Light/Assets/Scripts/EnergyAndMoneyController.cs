using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnergyAndMoneyController : MonoBehaviour
{
    private SaveData _saveData;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI money;
    public TextMeshProUGUI energyTimer;
    public GameObject fake;
    private int _energy, _money;

    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _energy = _saveData.save.energy;
        _money = _saveData.save.money;
        energy.text = _energy.ToString();
        money.text = _money.ToString();
    }
    
    private void FixedUpdate()
    {
        money.text = _money.ToString();
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
        if (_energy == 30)
        {
            _saveData.save.energySave = 0;
            _saveData.save.energyTimer = 0;
            _saveData.SetDateTime(DateTime.UtcNow);
            return;
        }
        LoadData();
        _energyToRespond = 30 - _energy;
        
        if (_energyToRespond <= 0)
        {
            fake.SetActive(true);
            energyTimer.text = 0.ToString();
            return;
        }
        
        _energyTimer += 59 * (_energyToRespond - _energySave);
        _energySave = _energyToRespond;

        var secondsPassed = (int) (DateTime.UtcNow - (_saveData.GetDateTime(DateTime.UtcNow))).TotalSeconds;

        if (_energyTimer >= 0)
        {
            _energyTimer -= secondsPassed;
            
            if (_energyTimer <= 59 * (_energyToRespond - 1))
            {
                _energy++;
                _energySave--;
            }
        }
        
        energyTimer.text = (_energyTimer - 59 * (_energyToRespond - 1)).ToString();

        fake.SetActive((_energyTimer - 59 * (_energyToRespond - 1)) < 10);
        SaveData();
    }
}
