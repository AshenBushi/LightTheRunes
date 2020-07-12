using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainMenu
{
    public class FieldsController : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;

        private const float Speed = 50f;
        private readonly Vector2 _target = new Vector2(0f, 0f), 
            _leftTarget = new Vector2(-10.8f, 0f), 
            _rightTarget = new Vector2(10.8f, 0f),
            _safeTarget = new Vector2(0f, 25f);
        
        public GameObject[] fields;
        public TextMeshProUGUI fieldsName;
        public Sprite enable4X4;
        public Sprite enable5X5;
        private int _fieldEnable = 1;
        private bool _canMove = false, _next = false;
        private float _timer = 0f;
        private Vector3 _preTarget;

        private void Start()
        { 
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
            fields[_saveData.save.fieldPos].transform.position = _target;
            if (_saveData.save.currentLevel > 19)
            {
                fields[1].GetComponent<Button>().interactable = true;
                fields[1].GetComponent<Image>().sprite = enable4X4;
                _fieldEnable = 2;
            }
            if (_saveData.save.currentLevel > 40)
            {
                fields[2].GetComponent<Button>().interactable = true;
                fields[2].GetComponent<Image>().sprite = enable5X5;
                _fieldEnable = 3;
            }
        }

        private void FixedUpdate()
        {
            switch (_saveData.save.fieldPos) // Change 
            {
                case 0:
                    fieldsName.text = "3x3";
                    break;
                case 1:
                    fieldsName.text = "4x4";
                    break;
                case 2:
                    fieldsName.text = "5x5";
                    break;
            }

            Move();
            
            if(_timer > 0)
                _timer -= Time.deltaTime;
        }
        
        public void NextField()
        {
            if (!(_timer <= 0)) return;
            if (_saveData.save.fieldPos >= 2) return;
            _saveData.save.fieldPos += 1;
            _next = true;
            _timer = 0.2f;
            StartMove(_rightTarget, _leftTarget);
        }
        
        public void PreField()
        {
            if (!(_timer <= 0)) return;
            if (_saveData.save.fieldPos <= 0) return;
            _saveData.save.fieldPos -= 1;
            _next = false;
            _timer = 0.2f;
            StartMove(_leftTarget, _rightTarget);
        }
        
        public void Play()
        {
            if (_saveData.save.energy > 0)
            {
                _saveData.save.energy--;

                var i = 61;
                while (_saveData.save.levelStar[i] < 1)
                {
                    if (i == 0)
                    {
                        i = -1;
                        break;
                    }
                    i--;
                }
                switch (EventSystem.current.currentSelectedGameObject.name)
                {
                    case "Play3x3":
                        _saveData.save.currentLevel = i < 19 ? i + 1 : 20;
                        break;
                    case "Play4x4":
                        _saveData.save.currentLevel = i < 40 ? i + 1 : 41;
                        break;
                    default:
                        _saveData.save.currentLevel = i < 61 ? i + 1 : 62;
                        break;
                }
                _functions.ToScene("Gameplay");
            }
            else
            {
                _functions.EmptyEnergy();
            }
        }
        
        private void StartMove(Vector3 spawn, Vector3 preTarget)
        {
            _canMove = true;
            fields[_saveData.save.fieldPos].transform.position = spawn;
            _preTarget = preTarget;
        }

        private void Move()
        {
            if (!_canMove) return;

            Functions.MoveTo(_next ? fields[_saveData.save.fieldPos - 1] : fields[_saveData.save.fieldPos + 1], _preTarget, Speed);
            Functions.MoveTo(fields[_saveData.save.fieldPos], _target, Speed);

            if (fields[_saveData.save.fieldPos].transform.position != new Vector3(0f, 0f, 0f)) return;
            _canMove = false;
            if (_next)
                fields[_saveData.save.fieldPos - 1].transform.position = _safeTarget;
            else
                fields[_saveData.save.fieldPos + 1].transform.position = _safeTarget;
        }
    }
}
