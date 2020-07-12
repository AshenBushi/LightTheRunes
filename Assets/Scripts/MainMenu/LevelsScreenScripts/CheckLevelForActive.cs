using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckLevelForActive : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;
    
    private readonly GameObject[] _stars = new GameObject[3];
    public Sprite buttonOn;
    public Sprite eternityOn;
    public Sprite starOn;
    private int _counts;


    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
        
        if (gameObject.name != "0")
        {
            var preLevel = int.Parse(gameObject.name) - 1;
            if (_saveData.save.levelStar[preLevel] < 1) return;
        }

        _counts = _saveData.save.levelStar[int.Parse(gameObject.name)];
        for (var i = 0; i < 3; i++)
        {
            _stars[i] = gameObject.transform.Find("Star" + (i + 1).ToString()).gameObject;
            if(_stars[i] != null) 
                _stars[i].SetActive(true);
            if (i < _counts)
                _stars[i].GetComponent<Image>().sprite = starOn;
        }
        
        if (gameObject.name == "20" || gameObject.name == "41" || gameObject.name == "62")
            gameObject.GetComponent<Image>().sprite = eternityOn;
        else 
            gameObject.GetComponent<Image>().sprite = buttonOn;
        var levelName = gameObject.transform.Find("Text").gameObject;
        levelName.SetActive(true);
        gameObject.GetComponent<Button>().interactable = true;
    }
}
