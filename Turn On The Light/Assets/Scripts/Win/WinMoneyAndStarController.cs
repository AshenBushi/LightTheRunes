using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMoneyAndStarController : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;

    public TextMeshProUGUI winMoney;
    public SpriteRenderer[] stars;
    public Sprite starOn;
    private int _winMoney;

    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();

        _winMoney = _saveData.save.winMoney;
        winMoney.text = _winMoney.ToString();
        _saveData.save.money += _winMoney;

        for (var i = 0; i < _saveData.save.levelStar[_saveData.save.currentLevel]; i++)
        {
            stars[i].sprite = starOn;
        }
        
        _saveData.save.levelStar[_saveData.save.currentLevel] = 0;
    }

}
