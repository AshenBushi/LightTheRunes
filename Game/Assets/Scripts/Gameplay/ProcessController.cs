using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ProcessController : MonoBehaviour
{
    [SerializeField] List<PadController> pads; 
    public Slider energyBar; 
    public TextMeshProUGUI energy;
    private float _value;
    private int _pause, _money, _level;

    private void Start()
    {
        PlayerPrefs.SetInt("MenuActive", 0);
    }

    private void FixedUpdate()
    {
        _pause = PlayerPrefs.GetInt("MenuActive");
        if(_pause != 1)
        {
            _value = energyBar.value - Time.deltaTime;// * 16.66f;
            energy.text = Mathf.RoundToInt(_value / 1).ToString();
            energyBar.value = _value;
        }

        if(_value <= 0)
            SceneManager.LoadScene("LoseMenu");
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
        _level = PlayerPrefs.GetInt("Level");
        if(_value >= energyBar.maxValue * 0.75 && StarsSavingSystem.Get(_level) < 3)
        {
            if(PlayerPrefs.GetInt("BonusLevel") == 1)
                for(var i = StarsSavingSystem.Get(_level); i < 3; i++)
                    _money += 30;
            else
                for(var i = StarsSavingSystem.Get(_level); i < 3; i++)
                    _money += 10;
            StarsSavingSystem.Edit(_level, 3);
        }
        else
            if(_value >= energyBar.maxValue * 0.4 && StarsSavingSystem.Get(_level) < 2)
            {
                if(PlayerPrefs.GetInt("BonusLevel") == 1)
                    for(var i = StarsSavingSystem.Get(_level); i < 2; i++)
                        _money += 30;
                else
                    for(var i = StarsSavingSystem.Get(_level); i < 2; i++)
                        _money += 10;
                StarsSavingSystem.Edit(_level, 2);
            }
            else
                if(StarsSavingSystem.Get(_level) < 1)
                {
                    if(PlayerPrefs.GetInt("BonusLevel") == 1)
                        for(var i = StarsSavingSystem.Get(_level); i < 1; i++)
                            _money += 30;
                    else
                        for(var i = StarsSavingSystem.Get(_level); i < 1; i++)
                            _money += 10;
                    StarsSavingSystem.Edit(_level, 1);   
                } 

        PlayerPrefs.SetInt("WinMoney", _money);
        PlayerPrefs.SetInt("WinEnergy", Mathf.RoundToInt(energyBar.value));
        if(_level >= PlayerPrefs.GetInt("CurrentLevel") && _level  < 59)
            PlayerPrefs.SetInt("CurrentLevel", _level + 1);
        SceneManager.LoadScene("WinMenu");
    }

}
