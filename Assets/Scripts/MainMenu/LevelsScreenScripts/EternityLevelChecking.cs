using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EternityLevelChecking : MonoBehaviour
{
    private LevelStarsShow _levelStarsShow;
    
    [SerializeField] private Sprite buttonOn;
    [SerializeField] private GameObject[] elementsToOff;

    private void Start()
    {
        _levelStarsShow = GetComponentInParent<LevelStarsShow>();

        if (_levelStarsShow.sum < 25) return;

        foreach (var element in elementsToOff)
        {
            element.SetActive(false);
        }
        gameObject.GetComponent<Image>().sprite = buttonOn;
        gameObject.GetComponent<Button>().interactable = true;
    }
}
