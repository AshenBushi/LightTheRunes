using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockStarsShow : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject fake;
    public TextMeshProUGUI starsSum;
    private int _sum = 0;
    private void Start()
    {
        for (var i = 0; i < 20; i++)
        {
            _sum += StarsSavingSystem.Get(int.Parse(levels[i].name));
        }
        if(_sum >= 10)
            fake.SetActive(false);
        starsSum.text = _sum.ToString();
    }
}
