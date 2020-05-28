using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonsControll : MonoBehaviour
{
    public Button next;
    public void FixedUpdate()
    {
        next.interactable = PlayerPrefs.GetInt("Energy") > 0;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        var level = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
        PlayerPrefs.SetInt("Level", level);
        LoadGameScene(level);
    }


    public void Reset()
    {
        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
        var level = PlayerPrefs.GetInt("Level");
        LoadGameScene(level);
    }
    private static void LoadGameScene(int level)
    {
        if (level < 21)
            SceneManager.LoadScene("3x3");
        else if (level < 42)
            SceneManager.LoadScene("4x4");
        else
            SceneManager.LoadScene("5x5");

    }
}
