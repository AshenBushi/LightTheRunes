using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelStarsShow : MonoBehaviour
{
    private readonly GameObject[] _stars = new GameObject[3];
    public Sprite starOn;
    private int _counts;

    private void Start()
    {
        _counts = StarsSavingSystem.Get(int.Parse(gameObject.name));
        for (var i = 0; i < _counts; i++)
        {
            _stars[i] = gameObject.transform.Find("Star" + (i + 1).ToString()).gameObject;
            _stars[i].GetComponent<SpriteRenderer>().sprite = starOn;
        }
    }
    
}