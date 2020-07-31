using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    private SessionData _sessionData;
    private ShopData _shopData;

    private void Start()
    {
        _sessionData = FindObjectOfType<SessionData>();
        _shopData = FindObjectOfType<ShopData>();
        
        if (_shopData.shopSave.magicScroll == 0)
            magicScrollButton.interactable = false;
        if (_shopData.shopSave.manaPotion == 0)
            manaPotionButton.interactable = false;
    }

    public Button magicScrollButton;
    
    public void MagicScroll()
    {
        if (_shopData.shopSave.magicScroll < 1) return;
        _sessionData.sessionSave.magicScrollUse = !_sessionData.sessionSave.magicScrollUse;
        if (_shopData.shopSave.magicScroll == 1)
            magicScrollButton.interactable = false;
        _shopData.shopSave.magicScroll--;
    }

    public Slider energyBar;
    public Button manaPotionButton;
    
    public void ManaPotion()
    {
        if (_shopData.shopSave.manaPotion < 1) return;
        energyBar.value += energyBar.maxValue / 2;
        if (_shopData.shopSave.manaPotion == 1)
            manaPotionButton.interactable = false;
        _shopData.shopSave.manaPotion--;
    }
}
