using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSet : MonoBehaviour
{
    public PadController[] pads;
    public GameObject[] levelsPic;
    public TextMeshProUGUI levelsCount;
    public Sprite @on;
    private List<int> _nums; 
    public int level;
    private int _j;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        if (level > 20)
        {
            level -= 21;
            levelsCount.text = (1 + level).ToString();
            level += 21;
        }
        else
        {
            levelsCount.text = (1 + level).ToString();
        }

        if (level == 20 || level == 41 || level == 62)
        {
            levelsPic[0].SetActive(false);
            levelsPic[1].SetActive(true);
        }

        switch (level)
        {
            case 0: //Block #1
                WhiteX2(7, 8);
                break;
            case 1:
                WhiteX3(2, 4, 6);
                break;
            case 2:
                WhiteX3(3, 4, 5);
                break;
            case 3:
                WhiteX1(5);
                break;
            case 4:
                WhiteX2(2, 4); 
                break;
            case 5:
                WhiteX3(1, 4, 5);
                break;
            case 6:
                WhiteX2(3, 5);
                break;
            case 7:
                WhiteX4(0, 1, 2, 4);
                WhiteX1(7);
                break;
            case 8:
                WhiteX2(0, 2);
                break;
            case 9:
                WhiteX2(0, 5);
                break;
            case 10:
                WhiteX3(5, 7, 8);
                break;
            case 11:
                WhiteX4(0, 2, 3, 7);
                break;
            case 12:
                WhiteX3(1, 2, 3);
                break;
            case 13:
                WhiteX1(7);
                break;
            case 14:
                WhiteX1(8);
                break;
            case 15:
                WhiteX2(1, 3);
                break;
            case 16:
                WhiteX3(3, 4, 6);
                break;
            case 17:
                WhiteX3(0, 2, 7);
                break; 
            case 18:
                WhiteX2(2, 6);
                break; 
            case 19:
                WhiteX3(3, 5, 8);
                break;
            case 20:
                Random3X3();
                break;
            case 21: //Block #2
                WhiteX4(5, 6, 9, 10);
            break;
            case 22:
                WhiteX2(6, 12);
            break;
            case 23:
                WhiteX4(0, 1, 3, 13);
            break;
            case 24:
                WhiteX4(0, 8, 11, 12);
            break;
            case 25:
                WhiteX4(10, 13, 14, 15);
            break;
            case 26:
                WhiteX2(0, 10);
            break;
            case 27:
                WhiteX3(2, 7, 9);
            break;
            case 28:
                WhiteX4(1, 2, 5, 6);
                WhiteX4(9, 10, 13, 14);
            break;
            case 29:
                WhiteX4(3, 5, 10, 12);
            break;
            case 30:
                WhiteX4(0, 3, 12, 15);
            break;
            case 31:
                WhiteX4(3, 10, 12, 15);
                break;
            case 32:
                WhiteX4(5, 8, 9, 14);
                WhiteX1(15);
            break; 
            case 33:
                WhiteX4(3, 7, 9, 13);
                WhiteX1(15);
            break; 
            case 34:
                WhiteX4(0, 4, 6, 7);
                WhiteX4(8, 9, 11 ,15);
            break; 
            case 35:
                WhiteX4(2, 5, 7, 8);
                WhiteX1(13);
                break; 
            case 36:
                WhiteX4(1, 2, 4, 7);
                WhiteX3(11, 15, 14);
                break;
            case 37:
                WhiteX3(6, 8, 13);
            break; 
            case 38:
                WhiteX3(1, 9, 11);
                break; 
            case 39:
                WhiteX4(2, 5, 7, 10);
                WhiteX1(12);
            break; 
            case 40:
                WhiteX4(3, 8, 11, 15);
            break;      
        }

    }

    private void WhiteX1(int x0)
    {
        pads[x0].isTurn = !pads[x0].isTurn;
        pads[x0].GetComponent<SpriteRenderer>().sprite = @on;
    }

    private void WhiteX2(int x0, int x1)
    {
        pads[x0].isTurn = !pads[x0].isTurn;
        pads[x0].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x1].isTurn = !pads[x1].isTurn;
        pads[x1].GetComponent<SpriteRenderer>().sprite = @on;
    }

    private void WhiteX3(int x0, int x1, int x2)
    {
        pads[x0].isTurn = !pads[x0].isTurn;
        pads[x0].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x1].isTurn = !pads[x1].isTurn;
        pads[x1].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x2].isTurn = !pads[x2].isTurn;
        pads[x2].GetComponent<SpriteRenderer>().sprite = @on;
    }

    private void WhiteX4(int x0, int x1, int x2, int x3)
    {
        pads[x0].isTurn = !pads[x0].isTurn;
        pads[x0].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x1].isTurn = !pads[x1].isTurn;
        pads[x1].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x2].isTurn = !pads[x2].isTurn;
        pads[x2].GetComponent<SpriteRenderer>().sprite = @on;
        pads[x3].isTurn = !pads[x3].isTurn;
        pads[x3].GetComponent<SpriteRenderer>().sprite = @on;
    }

    private void Random3X3()
    {
        var blockCount = Random.Range(1, 5);
        var blocks = new int[blockCount];
        Debug.Log(blockCount);
        for (var i = 0; i < blockCount; i++)
        {
            blocks[i] = Random.Range(0, 9);
            Debug.Log("i = " + i.ToString());
            Debug.Log("Block = " + blocks[i].ToString());
            for(var j = 0; j < blockCount; j++)
                if (blocks[i] == blocks[j] && i != j)
                {
                    blocks[i] = Random.Range(0, 9);
                    Debug.Log("i = " + i.ToString());
                    Debug.Log("Block = " + blocks[i].ToString());
                    j = 0;
                }
        }

        switch (blockCount)
        {
            case 1:
                WhiteX1(blocks[0]);
                break;
            case 2:
                WhiteX2(blocks[0], blocks[1]);
                break;
            case 3:
                WhiteX3(blocks[0], blocks[1], blocks[2]);
                break;
            case 4:
                WhiteX4(blocks[0], blocks[1], blocks[2], blocks[3]);
                break;
        }
    }
}
