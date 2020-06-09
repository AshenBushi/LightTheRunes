using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

public class Functions : MonoBehaviour
{
    private SaveData _saveData;

    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
    }

    public void ToScene(string scene)
    {
        _saveData.Save();
        SceneManager.LoadScene(scene);
    }

    public static void MoveTo(GameObject obj, Vector2 target, float speed)
    {
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
    }
}
