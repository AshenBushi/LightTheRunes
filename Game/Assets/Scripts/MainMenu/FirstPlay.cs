using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPlay : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("FirstPlay")) return;
        PlayerPrefs.SetInt("SoundStatus", 1);
        PlayerPrefs.SetInt("FirstPlay", 1);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.SetInt("Money", 500);
        PlayerPrefs.SetInt("Energy", 27);
        PlayerPrefs.SetInt("Battery", 0);
        PlayerPrefs.SetInt("Lamp", 0);
        PlayerPrefs.SetInt("Clock", 0);
        PlayerPrefs.SetInt("Extra", 0);
        PlayerPrefs.SetInt("EnergyTimer", 0);
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        //SceneManager.LoadScene("MainScene");
    }
}
