using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class Functions : MonoBehaviour
{
    private SaveData _saveData;
    public GameObject getEnergy;

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

    public void EmptyEnergy()
    {
        _saveData.save.pause = true;
        getEnergy.SetActive(true);
    }
    
    public int[] RandomLevel(int counts, int maxValue)
    {
        var blockCount = Random.Range(1, counts);
        var blocks = new int[blockCount];
        for (var i = 0; i < blockCount; i++)
        {
            blocks[i] = Random.Range(0, maxValue);
            for(var j = 0; j < blockCount; j++)
                if (blocks[i] == blocks[j] && i != j)
                {
                    blocks[i] = Random.Range(0, maxValue);
                    j = 0;
                }
        }

        return blocks;
    }
}
