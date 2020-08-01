using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelScreenManager : MonoBehaviour
{
    private ProgressData _progressData;
    private Functions _functions;

    private void Start()
    {
        _progressData = FindObjectOfType<ProgressData>();
        _functions = FindObjectOfType<Functions>();
    }

    [SerializeField] private GameObject levelBlocksNavigation;
    [SerializeField] private List<GameObject> levelBlocks;
    
    public void ChooseLevelsBlock(int blockNumber)
    {
        levelBlocksNavigation.SetActive(false);

        switch (blockNumber)
        {
            case 1:
                levelBlocks[0].SetActive(true);
                break;
            case 2:
                levelBlocks[1].SetActive(true);
                break;
            case 3:
                levelBlocks[2].SetActive(true);
                break;
        }
    }

    public void BackToNavigation()
    {
        levelBlocksNavigation.SetActive(true);
        
        for(var i = 0; i < 3; i++)
            levelBlocks[i].SetActive(false);
    }

    public void GoToLevel()
    {
        if (_progressData.progressSave.energy > 0)
        {
            _progressData.progressSave.energy --;
            _progressData.progressSave.currentLevel = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            _functions.ToScene("Gameplay");
        }
        else
        {
            _functions.EmptyEnergy();
        }
    }
}
