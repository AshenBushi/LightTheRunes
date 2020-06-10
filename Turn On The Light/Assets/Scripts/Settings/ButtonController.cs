using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;

    public Image[] buttons;
    public Sprite buttonOn;
    public Sprite buttonOff;

    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
    }

    private void FixedUpdate()
    {
        buttons[0].sprite = _saveData.save.sound ? buttonOn : buttonOff;
        buttons[1].sprite = _saveData.save.music ? buttonOn : buttonOff;
    }

    public void Exit()
    {
        _functions.ToScene("MainMenu");
    }
    
    public void MusicChange()
    {
        _saveData.save.music = !_saveData.save.music;
    }
    
    public void SoundChange()
    {
        _saveData.save.sound = !_saveData.save.sound;
    }
}
