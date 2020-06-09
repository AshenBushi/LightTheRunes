using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class FieldAndLevelSet : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;
        
        public GameObject[] fields;
        private int _currentField;

        private void Awake()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }

        private void Start()
        {
            _currentField = _saveData.save.currentLevel;
            fields[_currentField].transform.position = _currentField == 0 ? new Vector3(0f, -1f, 0f) : new Vector3(-0.03f, -0.96f, 0f);
        }
    }
}
