using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsStarShow : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;
    
    public GameObject[] levels;
    public GameObject fake;
    public TextMeshProUGUI starsSum;
    private int _sum = 0;
    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
        
        for (var i = 0; i < 20; i++)
        {
            _sum += _saveData.save.levelStar[int.Parse(levels[i].name)];
        }
        if(_sum >= 10)
            fake.SetActive(false);
        starsSum.text = _sum.ToString();
    }
}
