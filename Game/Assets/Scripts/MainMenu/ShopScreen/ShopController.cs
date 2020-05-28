using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private int _money, _battery, _lamp, _clock, _extra;

    private void Update()
    {
        _money = PlayerPrefs.GetInt("Money");
        _battery = PlayerPrefs.GetInt("Battery");
        _lamp = PlayerPrefs.GetInt("Lamp");
        _clock = PlayerPrefs.GetInt("Clock");
        _extra = PlayerPrefs.GetInt("Extra");
    }

    public void BuyBattery()
    {
        if (_money < 50) return;
        _money -= 50;
        _battery += 1;
        PlayerPrefs.SetInt("Money", _money);
        PlayerPrefs.SetInt("Battery", _battery);
    }
    
    public void BuyLamp()
    {
        if (_money < 150) return;
        _money -= 150;
        _lamp += 1;
        PlayerPrefs.SetInt("Money", _money);
        PlayerPrefs.SetInt("Lamp", _lamp);
    }
    
    public void BuyClock()
    {
        if (_money < 300) return;
        _money -= 300;
        _clock += 1;
        PlayerPrefs.SetInt("Money", _money);
        PlayerPrefs.SetInt("Clock", _clock);
    }
    
    public void BuyExtra()
    {
        if (_money < 500) return;
        _money -= 500;
        _battery += 1;
        PlayerPrefs.SetInt("Money", _money);
        PlayerPrefs.SetInt("Extra", _extra);
    }
}
