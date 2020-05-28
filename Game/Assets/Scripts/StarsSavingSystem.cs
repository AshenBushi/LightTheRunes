using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public static class StarsSavingSystem
{
    private const int LevelsCount = 65;
    static Stars _sm = new Stars();
    private static Stars Load()
    { 
        if (PlayerPrefs.HasKey("SaveStars")) return JsonUtility.FromJson<Stars>(PlayerPrefs.GetString("SaveStars"));
            for(var i = 0; i < LevelsCount; i++)
                _sm.starsMas[i] = 0; 
        PlayerPrefs.SetString("SaveStars", JsonUtility.ToJson(_sm));
        return JsonUtility.FromJson<Stars>(PlayerPrefs.GetString("SaveStars"));
    }

    private static void Save()
    { 
        PlayerPrefs.SetString("SaveStars", JsonUtility.ToJson(_sm));
    }
    
    public static void Edit(int level, int stars)
    {
        _sm = Load(); 
        _sm.starsMas[level] = stars; 
        Save();
    }

    public static int Get(int level)
    { 
        _sm = Load(); 
        return _sm.starsMas[level];
    }

    [Serializable]
    public class Stars
    {
        public int[] starsMas = new int[LevelsCount];
    }
}
