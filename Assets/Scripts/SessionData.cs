using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SessionData : MonoBehaviour
{
private string _path;
    public SeData sessionSave = new SeData();

    private void Awake()
    {
        Load();
        sessionSave.pause = false;
    }

    private void Load()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "SessionSave.json");
#else
        _path = Path.Combine(Application.dataPath, "SessionSave.json");
#endif
        if (File.Exists(_path))
        {
            sessionSave = JsonUtility.FromJson<SeData>(File.ReadAllText(_path));
        }
        else
        {
            FirstPlay();
            File.WriteAllText(_path, JsonUtility.ToJson(sessionSave));
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        File.Delete(_path);
        File.Delete(Path.Combine(Application.dataPath, "SessionSave.json.meta"));
    }
#endif
    private void OnApplicationQuit()
    {
        File.Delete(_path);
        File.Delete(Path.Combine(Application.dataPath, "SessionSave.json.meta"));
    }

    public void Save()
    {
        File.WriteAllText(_path, JsonUtility.ToJson(sessionSave));
    }
    
    private void FirstPlay()
    {
        sessionSave.pause = false;
        sessionSave.winMoney = 0;
        sessionSave.cameraPos = File.Exists(Path.Combine(Application.persistentDataPath, "ProgressSave.json")) ? 2 : 0;
        sessionSave.magicScrollUse = false;
    }
}

[Serializable]
public class SeData
{
    public bool pause;
    public int winMoney;
    public int cameraPos;
    public bool magicScrollUse;
}

