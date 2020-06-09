using System.Collections;
using TMPro;
using UnityEngine;

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
        private int _fieldPos;
        private bool _canMove = false, _next = false;
        private float _timer = 0f;
        private Vector3 _preTarget;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
            _fieldPos = _saveData.save.fieldPos;
            fields[_fieldPos].transform.position = _target;
        }

        private void FixedUpdate()
        {
            switch (_fieldPos) // Change 
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
            if (_fieldPos >= 2) return;
            _fieldPos += 1;
            _next = true;
            _timer = 0.2f;
            StartMove(_rightTarget, _leftTarget);
        }
        
        public void PreField()
        {
            if (!(_timer <= 0)) return;
            if (_fieldPos <= 0) return;
            _fieldPos -= 1;
            _next = false;
            _timer = 0.2f;
            StartMove(_leftTarget, _rightTarget);
        }
        
        public void Play()
        {
            if(_saveData.save.energy < 1) return;
            _saveData.save.energy--;
            _saveData.save.fieldPos = _fieldPos;
            _saveData.save.currentLevel = _fieldPos;
            _functions.ToScene("Gameplay");
        }
        
        private void StartMove(Vector3 spawn, Vector3 preTarget)
        {
            _canMove = true;
            fields[_fieldPos].transform.position = spawn;
            _preTarget = preTarget;
        }

        private void Move()
        {
            if (!_canMove) return;

            Functions.MoveTo(_next ? fields[_fieldPos - 1] : fields[_fieldPos + 1], _preTarget, Speed);
            Functions.MoveTo(fields[_fieldPos], _target, Speed);

            if (fields[_fieldPos].transform.position != new Vector3(0f, 0f, 0f)) return;
            _canMove = false;
            if (_next)
                fields[_fieldPos - 1].transform.position = _safeTarget;
            else
                fields[_fieldPos + 1].transform.position = _safeTarget;
        }
    }
}
