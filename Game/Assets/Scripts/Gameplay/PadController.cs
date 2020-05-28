using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PadController : MonoBehaviour
{
    public Sprite on; public Sprite off; public Slider energyBar; public TextMeshProUGUI energy;
    [SerializeField] private PadController[] padsIteractWith;
    public bool isTurn = false;
    private int _menuActive, _move, _level; 
    private float _value;
    private readonly int[] _bonusLevel = {17, 18, 19, 37, 38, 39, 57, 58, 59};

    private float _timer = 0.2f;
    public AudioSource sound;

    private void Start()
    {
        PlayerPrefs.SetInt("BonusLevel", 0);
        _level = PlayerPrefs.GetInt("Level");
        foreach(var check in _bonusLevel)
        {
            if(check == _level)
                PlayerPrefs.SetInt("BonusLevel", 1);
        }
    }

    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;
    }
    private void OnMouseDown()
    {
        _menuActive = PlayerPrefs.GetInt("MenuActive");
        if (_menuActive != 0 || !(_timer <= 0)) return;
        foreach(var padController in padsIteractWith)
        {
            padController.isTurn = !padController.isTurn;
            padController.GetComponent<SpriteRenderer>().sprite = padController.isTurn ? on : off;
        }

        if(PlayerPrefs.GetInt("BonusLevel") == 1)
        {
            _value = energyBar.value - 10;
            energy.text = Mathf.RoundToInt(_value / 1).ToString();
            energyBar.value = _value;
        }
            
        sound.Play();
        FindObjectOfType<ProcessController>().CheckForWin();
        _timer = 0.2f;
    }
}
