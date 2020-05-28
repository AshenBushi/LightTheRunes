using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Image soundChange;
    public Sprite soundOn;
    public Sprite soundOff;
    private int _soundStatus;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        //StartCoroutine(SoundSet());
    }

    public void ChangeSound()
    {
        _soundStatus = PlayerPrefs.GetInt("SoundStatus");
        if (_soundStatus == 1)
        {
            soundChange.sprite = soundOff;
            gameObject.GetComponent<AudioSource>().mute = true;
            PlayerPrefs.SetInt("SoundStatus", 0);
        }
        else
        {
            soundChange.sprite = soundOn;
            gameObject.GetComponent<AudioSource>().mute = false;
            PlayerPrefs.SetInt("SoundStatus", 1);
        }
    }
    
    /*private IEnumerator SoundSet()
    {
        if(!PlayerPrefs.HasKey("FirstPlay"))
            yield return new WaitForSeconds(0.05f);
        _soundStatus = PlayerPrefs.GetInt("SoundStatus");
        if (_soundStatus == 1)
        {
            soundChange.sprite = soundOn;
            _audioSource.mute = false;
        }
        else
        {
            soundChange.sprite = soundOff;
            _audioSource.mute = true;
        }
    }*/
}
