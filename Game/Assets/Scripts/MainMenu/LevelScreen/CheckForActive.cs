using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CheckForActive : MonoBehaviour
{
    private readonly GameObject[] _stars = new GameObject[3];
    public Sprite buttonOn;

    private void Start()
    {
        var preLevel = int.Parse(gameObject.name) - 1;
        if (StarsSavingSystem.Get(preLevel) < 1) return;
        for (var i = 0; i < 3; i++)
        {
            _stars[i] = gameObject.transform.Find("Star" + (i + 1).ToString()).gameObject;
            if(_stars[i] != null) 
                _stars[i].SetActive(true);
        }
        gameObject.GetComponent<Image>().sprite = buttonOn;
        var levelName = gameObject.transform.Find("LevelName").gameObject;
        levelName.SetActive(true);
        gameObject.GetComponent<Button>().interactable = true;
    }
}
