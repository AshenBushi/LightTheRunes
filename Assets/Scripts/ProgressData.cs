using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressData : MonoBehaviour
{
    private string _path;
    public PrData progressSave = new PrData();

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "ProgressSave.json");
#else
        _path = Path.Combine(Application.dataPath, "ProgressSave.json");
#endif

        if (File.Exists(_path))
        {
            progressSave = JsonUtility.FromJson<PrData>(File.ReadAllText(_path));
        }
        else
        {
            FirstPlay();
            File.WriteAllText(_path, JsonUtility.ToJson(progressSave));
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
        File.WriteAllText(_path, JsonUtility.ToJson(progressSave));
    }
    
    private void FirstPlay()
    {
        progressSave.energy = 30;
        progressSave.money = 0;
        progressSave.energyTimer = 0;
        progressSave.energySave = 0;
        progressSave.sound = true;
        progressSave.music = true;
        progressSave.date = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture);
        progressSave.currentLevel = 0;
        progressSave.levelStar = new int[100];
        for (var i = 0; i < 100; i++)
            progressSave.levelStar[i] = 0;
        progressSave.fieldPos = 0;
        progressSave.levelBlockPos = 0;
    }
    
    public void SetDateTime(DateTime value)
    {
        var convert = value.ToString("u", CultureInfo.InvariantCulture);
        progressSave.date = convert;
    }

    public DateTime GetDateTime(DateTime defaultValue)
    {
        if (progressSave.date != null)
        {
            var result = DateTime.ParseExact(progressSave.date, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
            return defaultValue;
    }
}

[Serializable]
public class PrData
{
    public int energy;
    public int money;
    public int energyTimer;
    public int energySave;
    public bool sound;
    public bool music;
    public string date;
    public int currentLevel;
    public int[] levelStar;
    public int fieldPos;
    public int levelBlockPos;
}