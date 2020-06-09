using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Test : MonoBehaviour
{
    public SaveData saveData;
    private void Start()
    {
        saveData = FindObjectOfType<SaveData>();
    }

    private void Update()
    {
        
    }
}
