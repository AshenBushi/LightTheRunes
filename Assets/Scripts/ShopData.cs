using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    private string _path;
    public ShData shopSave = new ShData();

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "ShopSave.json");
#else
        _path = Path.Combine(Application.dataPath, "ShopSave.json");
#endif

        if (File.Exists(_path))
        {
            shopSave = JsonUtility.FromJson<ShData>(File.ReadAllText(_path));
        }
        else
        {
            FirstPlay();
            File.WriteAllText(_path, JsonUtility.ToJson(shopSave));
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
        File.WriteAllText(_path, JsonUtility.ToJson(shopSave));
    }
    
    private void FirstPlay()
    {
        shopSave.magicScroll = 0;
        shopSave.manaPotion = 0;
        shopSave.extraLife = 0;
    }
}

[Serializable]
public class ShData
{
    public int magicScroll;
    public int manaPotion;
    public int extraLife;
}
