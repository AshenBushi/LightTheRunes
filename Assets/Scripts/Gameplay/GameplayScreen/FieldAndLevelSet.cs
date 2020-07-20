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
        private ProgressData _progressData;
        private Functions _functions;
        
        public GameObject[] fields;
        public PadController[] pads3X3;
        public PadController[] pads4X4;
        public PadController[] pads5X5;
        public Sprite[] padOn;
        public int fieldPos = 0;
        private int[] _pads;

        private void Start()
        {
            _progressData = FindObjectOfType<ProgressData>();
            _functions = FindObjectOfType<Functions>();

            if (_progressData.progressSave.currentLevel <= 20)
                fieldPos = 0;
            else if (_progressData.progressSave.currentLevel <= 41)
                fieldPos = 1;
            else
                fieldPos = 2;
            
            fields[fieldPos].transform.position = _progressData.progressSave.currentLevel == 0 ? new Vector3(0f, -1f, 0f) : new Vector3(-0.03f, -0.96f, 0f);
            
            for (var i = 0; i < 3; i++)
                if (i != fieldPos)
                    fields[i].SetActive(false);

            switch (_progressData.progressSave.currentLevel)
            {
                case 0: //Block #1
                    _pads = new[] {7, 8};
                    break;
                case 1:
                    _pads = new[] {2, 4, 6};
                    break;
                case 2:
                    _pads = new[] {3, 4, 5};
                    break;
                case 3:
                    _pads = new[] {5};
                    break;
                case 4:
                    _pads = new[] {2, 4}; 
                    break;
                case 5:
                    _pads = new[] {1, 4, 5};
                    break;
                case 6:
                    _pads = new[] {3, 5};
                    break;
                case 7:
                    _pads = new[] {0, 1, 2, 4, 7};
                    break;
                case 8:
                    _pads = new[] {0, 2};
                    break;
                case 9:
                    _pads = new[] {0, 5};
                    break;
                case 10:
                    _pads = new[] {5, 7, 8};
                    break;
                case 11:
                    _pads = new[] {0, 2, 3, 7};
                    break;
                case 12:
                    _pads = new[] {1, 2, 3};
                    break;
                case 13:
                    _pads = new[] {7};
                    break;
                case 14:
                    _pads = new[] {8};
                    break;
                case 15:
                    _pads = new[] {1, 3};
                    break;
                case 16:
                    _pads = new[] {3, 4, 6};
                    break;
                case 17:
                    _pads = new[] {0, 2, 7};
                    break; 
                case 18:
                    _pads = new[] {2, 6};
                    break; 
                case 19:
                    _pads = new[] {3, 5, 8};
                    break;
                case 20:
                    _pads = _functions.RandomLevel(6, 9);
                    break;
                case 21: //Block #2
                    _pads = new[] {5, 6, 9, 10};
                    break;
                case 22:
                    _pads = new[] {6, 12};
                    break;
                case 23:
                    _pads = new[] {0, 1, 3, 13};
                    break;
                case 24:
                    _pads = new[] {0, 8, 11, 12};
                    break;
                case 25:
                    _pads = new[] {10, 13, 14, 15};
                    break;
                case 26:
                    _pads = new[] {0, 10};
                    break;
                case 27:
                    _pads = new[] {2, 7, 9};
                    break;
                case 28:
                    _pads = new[] {1, 2, 5, 6, 9, 10, 13, 14};
                    break;
                case 29:
                    _pads = new[] {3, 5, 10, 12};
                    break;
                case 30:
                    _pads = new[] {0, 3, 12, 15};
                    break;
                case 31:
                    _pads = new[] {3, 10, 12, 15};
                    break;
                case 32:
                    _pads = new[] {5, 8, 9, 14, 15};
                    break; 
                case 33:
                    _pads = new[] {3, 7, 9, 13, 15};
                    break; 
                case 34:
                    _pads = new[] {0, 4, 6, 7, 8, 9, 11, 15};
                    break; 
                case 35:
                    _pads = new[] {1, 8, 15};
                    break; 
                case 36:
                    _pads = new[] {1, 2, 4, 7, 11, 15, 14};
                    break;
                case 37:
                    _pads = new[] {6, 8, 13};
                    break; 
                case 38:
                    _pads = new[] {1, 9, 11};
                    break; 
                case 39:
                    _pads = new[] {2, 5, 7, 10, 12};
                    break; 
                case 40:
                    _pads = new[] {3, 8, 11, 15};
                    break;
                case 41:
                    _pads = _functions.RandomLevel(8, 16);
                    break;
                case 42:
                    _pads = new[] {3, 5, 19, 21};
                    break;
                case 43:
                    _pads = new[] {7, 17};
                    break;
                case 44:
                    _pads = new[] {2, 4, 15, 20};
                    break;
                case 45:
                    _pads = new[] {11, 13};
                    break;
                case 46:
                    _pads = new[] {0, 2, 4, 7, 12};
                    break;
                case 47:
                    _pads = new[] {2, 6, 8, 10, 14, 16, 18, 22};
                    break;
                case 48:
                    _pads = new[] {12, 17, 20, 22, 24};
                    break;
                case 49:
                    _pads = new[] {0, 2, 4, 10, 14, 20, 22, 24};
                    break;
                case 50:
                    _pads = new[] {5, 6, 8, 9, 15, 16, 18, 19};
                    break;
                case 51:
                    _pads = new[] {1, 3, 6, 8, 16, 18, 21, 23};
                    break;
                case 52:
                    _pads = new[] {12};
                    break;
                case 53:
                    _pads = new[] {0, 2, 3, 9, 10, 14, 15, 21, 22, 24};
                    break;
                case 54:
                    _pads = new[] {1, 2, 3, 5, 9, 10, 14, 15, 19, 21, 22, 23};
                    break;
                case 55:
                    _pads = new[] {0, 1, 5, 12, 19, 23, 24};
                    break;
                case 56:
                    _pads = new[] {7, 11, 13, 17};
                    break;
                case 57:
                    _pads = new[] {1, 2, 7, 8, 9, 15, 16, 17, 22, 23};
                    break;
                case 58:
                    _pads = new[] {1, 5, 6, 18, 19, 23};
                    break;
                case 59:
                    _pads = new[] {3, 11, 13, 23};
                    break;
                case 60:
                    _pads = new[] {0, 2, 4, 6, 8, 10, 14, 16, 18, 20, 22, 24};
                    break;
                case 61:
                    _pads = _functions.RandomLevel(8, 25);
                    break;
                case 62:
                    _pads = _functions.RandomLevel(10, 25);
                    break;
            }
            
            switch (fieldPos)
            {
                case 0:
                    foreach (var t in _pads)
                    {
                        pads3X3[t].isTurn = !pads3X3[t].isTurn;
                        pads3X3[t].GetComponent<SpriteRenderer>().sprite = padOn[0];
                    }    
                    break;
                case 1:
                    foreach (var t in _pads)
                    {
                        pads4X4[t].isTurn = !pads4X4[t].isTurn;
                        pads4X4[t].GetComponent<SpriteRenderer>().sprite = padOn[1];
                    }    
                    break;
                case 2:
                    foreach (var t in _pads)
                    {
                        pads5X5[t].isTurn = !pads5X5[t].isTurn;
                        pads5X5[t].GetComponent<SpriteRenderer>().sprite = padOn[2];
                    }    
                    break;
            }
        }
    }
}
