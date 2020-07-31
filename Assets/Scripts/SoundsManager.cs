using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsManager: MonoBehaviour
{
    private ProgressData _progressData;

    public AudioSource mainTheme;
    public Image[] buttonsMusic;
    public Image[] buttonsSound;
    public Sprite imageOn;
    public Sprite imageOff;

    private void Start()
    {
        _progressData = FindObjectOfType<ProgressData>();
        mainTheme.mute = !_progressData.progressSave.music;
        for (var i = 0; i < 2; i++)
        {
            buttonsMusic[i].sprite = _progressData.progressSave.music ? imageOn : imageOff;
            buttonsSound[i].sprite = _progressData.progressSave.sound ? imageOn : imageOff;
        }
    }

    public void Sound()
    {
        _progressData.progressSave.sound = !_progressData.progressSave.sound;
        for (var i = 0; i < 2; i++)
        {
            buttonsSound[i].sprite = _progressData.progressSave.sound ? imageOn : imageOff;
        }
    }

    public void Music()
    {
        _progressData.progressSave.music = !_progressData.progressSave.music;
        mainTheme.mute = !_progressData.progressSave.music;
        for (var i = 0; i < 2; i++)
        {
            buttonsMusic[i].sprite = _progressData.progressSave.music ? imageOn : imageOff;
        }
    }
}
