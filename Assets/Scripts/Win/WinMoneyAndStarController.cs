using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMoneyAndStarController : MonoBehaviour
{
    private SessionData _sessionData;
    private ProgressData _progressData;
    private Functions _functions;

    public TextMeshProUGUI winMoney;
    public SpriteRenderer[] stars;
    public Sprite starOn;
    private int _winMoney;

    private void Start()
    {
        _sessionData = FindObjectOfType<SessionData>();
        _progressData = FindObjectOfType<ProgressData>();
        _functions = FindObjectOfType<Functions>();

        _winMoney = _sessionData.sessionSave.winMoney;
        winMoney.text = _winMoney.ToString();
        _progressData.progressSave.money += _winMoney;

        for (var i = 0; i < _progressData.progressSave.levelStar[_progressData.progressSave.currentLevel]; i++)
        {
            stars[i].sprite = starOn;
        }
    }

}
