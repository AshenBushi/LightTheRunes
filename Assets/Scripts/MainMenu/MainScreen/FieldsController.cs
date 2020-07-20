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
        private ProgressData _progressData;
        private SessionData _sessionData;
        private Functions _functions;

        private const float Speed = 50f;
        public Transform[] targets;
        
        public GameObject[] fields;
        public Sprite enable4X4;
        public Sprite enable5X5;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private int _fieldEnable = 1;
        private int _lastPassedLevel;
        private bool _canMove = false, _next = false;
        private float _timer = 0f;
        private Vector3 _preTarget;

        private void Start()
        { 
            _progressData = FindObjectOfType<ProgressData>();
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();
            
            fields[_progressData.progressSave.fieldPos].transform.position = targets[1].position;
            _lastPassedLevel = 61;
            while (_progressData.progressSave.levelStar[_lastPassedLevel] < 1)
            {
                if (_lastPassedLevel == 0)
                {
                    _lastPassedLevel = -1;
                    break;
                }
                _lastPassedLevel--;
            }
            if (_lastPassedLevel > 19)
            {
                fields[1].GetComponent<Button>().interactable = true;
                fields[1].GetComponent<Image>().sprite = enable4X4;
                _fieldEnable = 2;
            }
            if (_lastPassedLevel > 40)
            {
                fields[2].GetComponent<Button>().interactable = true;
                fields[2].GetComponent<Image>().sprite = enable5X5;
                _fieldEnable = 3;
            }
        }

        private void Update()
        {
            Move();
            DetectSwipe();
            if(_timer > 0)
                _timer -= Time.deltaTime;
        }

        private void DetectSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _endPoint = Input.mousePosition;
                
                CheckDirection(_startPoint, _endPoint);
            }
        }

        private void CheckDirection(Vector2 startPoint, Vector2 endPoint)
        {
            if (startPoint == endPoint || _sessionData.sessionSave.cameraPos != 2 || _sessionData.sessionSave.pause) return;
            
            if (endPoint.x < startPoint.x)
            {
                RightSwipe();
            }

            if (endPoint.x > startPoint.x)
            {
                LeftSwipe();
            }
        }

        private void LeftSwipe()
        {
            if (!(_timer <= 0)) return;
            if (_progressData.progressSave.fieldPos <= 0) return;
            _progressData.progressSave.fieldPos -= 1;
            _next = false;
            _timer = 0.2f;
            StartMove(targets[0].position, targets[2].position);
        }

        private void RightSwipe()
        {
            if (!(_timer <= 0)) return;
            if (_progressData.progressSave.fieldPos >= 2) return;
            _progressData.progressSave.fieldPos += 1;
            _next = true;
            _timer = 0.2f;
            StartMove(targets[2].position, targets[0].position);
        }
        
        public void Play()
        {
            if (_progressData.progressSave.energy > 0)
            {
                _progressData.progressSave.energy--;
                
                switch (EventSystem.current.currentSelectedGameObject.name)
                {
                    case "Play3x3":
                        _progressData.progressSave.currentLevel = _lastPassedLevel < 19 ? _lastPassedLevel + 1 : 20;
                        break;
                    case "Play4x4":
                        _progressData.progressSave.currentLevel = _lastPassedLevel < 40 ? _lastPassedLevel + 1 : 41;
                        break;
                    default:
                        _progressData.progressSave.currentLevel = _lastPassedLevel < 61 ? _lastPassedLevel + 1 : 62;
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
            fields[_progressData.progressSave.fieldPos].transform.position = spawn;
            _preTarget = preTarget;
        }

        private void Move()
        {
            if (!_canMove) return;

            Functions.MoveTo(_next ? fields[_progressData.progressSave.fieldPos - 1] : fields[_progressData.progressSave.fieldPos + 1], _preTarget, Speed);
            Functions.MoveTo(fields[_progressData.progressSave.fieldPos], targets[1].position, Speed);

            if (fields[_progressData.progressSave.fieldPos].transform.position != targets[1].position) return;
            _canMove = false;
            if (_next)
                fields[_progressData.progressSave.fieldPos - 1].transform.position = targets[3].position;
            else
                fields[_progressData.progressSave.fieldPos + 1].transform.position = targets[3].position;
        }

        
    }
}
