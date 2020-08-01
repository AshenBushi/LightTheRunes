using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelChecking : MonoBehaviour
{
    private ProgressData _progressData;
    
    [SerializeField] private Sprite buttonOn;
    [SerializeField] private Sprite starOn;
    private readonly GameObject[] _levelStars = new GameObject[3];
    private int _counts;


    private void Start()
    {
        _progressData = FindObjectOfType<ProgressData>();

        var levelNumber = int.Parse(gameObject.name);
        if (levelNumber != 0)
        {
            if (_progressData.progressSave.levelStar[levelNumber - 1] < 1) return;
        }

        _counts = _progressData.progressSave.levelStar[levelNumber];
        
        for (var i = 0; i < 3; i++)
        {
            _levelStars[i] = gameObject.transform.Find("Star" + (i + 1).ToString()).gameObject;
            if(_levelStars[i] != null) 
                _levelStars[i].SetActive(true);
            if (i < _counts)
                _levelStars[i].GetComponent<Image>().sprite = starOn;
        }
        
        gameObject.GetComponent<Image>().sprite = buttonOn;
        var levelName = gameObject.transform.Find("Text").gameObject;
        levelName.SetActive(true);
        gameObject.GetComponent<Button>().interactable = true;
    }
}
