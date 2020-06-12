using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class FieldAndLevelSet : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;
        
        public GameObject[] fields;
        public PadController[] pads3X3;
        public PadController[] pads4X4;
        public PadController[] pads5X5;
        public Sprite[] padOn;
        private int _currentField;
        private int[] _randomPads;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
            _currentField = _saveData.save.currentLevel;
            fields[_currentField].transform.position = _currentField == 0 ? new Vector3(0f, -1f, 0f) : new Vector3(-0.03f, -0.96f, 0f);
            
            for (var i = 0; i < 3; i++)
                if (i != _currentField)
                    fields[i].SetActive(false);

            switch (_currentField)
            {
                case 0 :
                    _randomPads = RandomLevel(6, 9);

                    foreach (var t in _randomPads)
                    {
                        pads3X3[t].isTurn = !pads3X3[t].isTurn;
                        pads3X3[t].GetComponent<SpriteRenderer>().sprite = padOn[0];
                    }
                    break;
                    
                case 1 :
                    _randomPads = RandomLevel(8, 16);

                    foreach (var t in _randomPads)
                    {
                        pads4X4[t].isTurn = !pads4X4[t].isTurn;
                        pads4X4[t].GetComponent<SpriteRenderer>().sprite = padOn[1];
                    }
                    break;
                
                case 2 :
                    _randomPads = RandomLevel(10, 25);

                    foreach (var t in _randomPads)
                    {
                        pads5X5[t].isTurn = !pads5X5[t].isTurn;
                        pads5X5[t].GetComponent<SpriteRenderer>().sprite = padOn[2];
                    }
                    break;
            }
        }
        
        private static int[] RandomLevel(int counts, int maxValue)
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
}
