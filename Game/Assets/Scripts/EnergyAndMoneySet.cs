using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAndMoneySet : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI energyTimer;
    public GameObject fakeTimer;
    private void FixedUpdate()
    {
        money.text = PlayerPrefs.GetInt("Money").ToString();
        energy.text = PlayerPrefs.GetInt("Energy").ToString();
        EnergyTimer();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("EnergySave", _energySave);
        PlayerPrefs.SetInt("EnergyTimer", _energyTimer);
        DataSave.SetDateTime(DateTime.UtcNow);
    }

    private void LoadData()
    {
        _energyTimer = PlayerPrefs.GetInt("EnergyTimer");
        if (_energyTimer < 0)
            _energyTimer = 0;
        _energy = PlayerPrefs.GetInt("Energy");
        _energySave = PlayerPrefs.GetInt("EnergySave");
    }

    private int _energyTimer, _energy, _energyToRespond, _energySave;
    
    private void EnergyTimer()
    {
        LoadData();
        _energyToRespond = 30 - _energy;
        
        if (_energyToRespond <= 0)
        {
            fakeTimer.SetActive(true);
            energyTimer.text = 0.ToString();
            return;
        }
        
        _energyTimer += 59 * (_energyToRespond - _energySave);
        _energySave = _energyToRespond;

        var secondsPassed = (int) (DateTime.UtcNow - (DataSave.GetDateTime(DateTime.UtcNow))).TotalSeconds;

        if (_energyTimer >= 0)
        {
            _energyTimer -= secondsPassed;
            
            if (_energyTimer <= 59 * (_energyToRespond - 1))
            {
                PlayerPrefs.SetInt("Energy", _energy + 1);
                _energySave--;
            }
        }
        
        energyTimer.text = (_energyTimer - 59 * (_energyToRespond - 1)).ToString();

        fakeTimer.SetActive((_energyTimer - 59 * (_energyToRespond - 1)) < 10);
        SaveData();
    }
    
    


}
