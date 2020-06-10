using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveData : MonoBehaviour
{
    private string _path;
    public Data save = new Data();

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        _path = Path.Combine(Application.dataPath, "Save.json");
#endif

        if (File.Exists(_path))
        {
            save = JsonUtility.FromJson<Data>(File.ReadAllText(_path));
        }
        else
        {
            FirstPlay();
            File.WriteAllText(_path, JsonUtility.ToJson(save));
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        Save();
    }
#endif
    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        File.WriteAllText(_path, JsonUtility.ToJson(save));
    }
    
    private void FirstPlay()
    {
        save.energy = 30;
        save.energyTimer = 0;
        save.money = 0;
        save.sound = true;
        save.music = false;
        save.date = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture);
        save.currentLevel = 0;
        save.fieldPos = 0;
        save.levelStar = new int[3];
        save.pause = false;
        save.testing = true;
    }
    
    public void SetDateTime(DateTime value)
    {
        var convert = value.ToString("u", CultureInfo.InvariantCulture);
        save.date = convert;
    }

    public DateTime GetDateTime(DateTime defaultValue)
    {
        if (save.date != null)
        {
            var result = DateTime.ParseExact(save.date, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
            return defaultValue;
    }
}

[Serializable]
public class Data
{
    public int energy;
    public int money;
    public int winMoney;
    public int energyTimer;
    public int energySave;
    public bool sound;
    public bool music;
    public string date;
    public int currentLevel;
    public int[] levelStar;
    public int fieldPos;
    public bool pause;
    public bool testing;
}