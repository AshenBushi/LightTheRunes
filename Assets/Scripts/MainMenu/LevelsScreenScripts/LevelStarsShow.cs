using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelStarsShow : MonoBehaviour
{
    private ProgressData _progressData;

    [SerializeField] private GameObject[] levels;
    [SerializeField] private TextMeshProUGUI starsSum;
    public int sum;
    private void OnEnable()
    {
        _progressData = FindObjectOfType<ProgressData>();

        foreach (var level in levels)
        {
            sum += _progressData.progressSave.levelStar[int.Parse(level.name)];
        }
        if(sum < 10)
            starsSum.text = "0"+sum.ToString();
        else
            starsSum.text = sum.ToString();
    }
}
