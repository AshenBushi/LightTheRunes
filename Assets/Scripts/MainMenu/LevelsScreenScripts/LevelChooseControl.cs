using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelChooseControl : MonoBehaviour
{
    private ProgressData _progressData;
    private SessionData _sessionData;
    private Functions _functions;
    
    private const float Speed = 50f;
    public Transform[] pointsToMove;
    
        public GameObject[] blocks;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private bool _canMove = false;
        private bool _next = false;
        private Vector3 _preTarget;
        private float _timer = 0f;

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
            if (startPoint == endPoint || _sessionData.sessionSave.cameraPos != 3 || _sessionData.sessionSave.pause) return;
        
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
            if (_progressData.progressSave.levelBlockPos <= 0) return;
            _progressData.progressSave.levelBlockPos -= 1;
            _next = false;
            _timer = 0.2f;
            StartMove(pointsToMove[0].position, pointsToMove[2].position);
        }

        private void RightSwipe()
        {
            if (!(_timer <= 0)) return;
            if (_progressData.progressSave.levelBlockPos >= 2) return;
            _progressData.progressSave.levelBlockPos += 1;
            _next = true;
            _timer = 0.2f;
            StartMove(pointsToMove[2].position, pointsToMove[0].position);
        }

        private void Start()
        {
            _progressData = FindObjectOfType<ProgressData>();
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();

            blocks[_progressData.progressSave.levelBlockPos].transform.position = pointsToMove[1].position;
        }

        private void Update()
        {
            Move();
            DetectSwipe();
            _timer -= Time.deltaTime;
        }
        
        private void StartMove(Vector3 spawn, Vector3 preTarget)
        {
            _canMove = true;
            blocks[_progressData.progressSave.levelBlockPos].transform.position = spawn;
            _preTarget = preTarget;
        }

        public void GoToLevel()
        {
            if (_progressData.progressSave.energy > 0)
            {
                _progressData.progressSave.energy --;
                _progressData.progressSave.currentLevel = int.Parse(EventSystem.current.currentSelectedGameObject.name);
                _functions.ToScene("Gameplay");
            }
            else
            {
                _functions.EmptyEnergy();
            }
        }
    
        private void Move()
        {
            if (!_canMove) return;

            Functions.MoveTo(_next ? blocks[_progressData.progressSave.levelBlockPos - 1] : blocks[_progressData.progressSave.levelBlockPos + 1], _preTarget, Speed);
            Functions.MoveTo(blocks[_progressData.progressSave.levelBlockPos], pointsToMove[1].position, Speed);

            if (blocks[_progressData.progressSave.levelBlockPos].transform.position != pointsToMove[1].position) return;
            _canMove = false;
            if (_next)
                blocks[_progressData.progressSave.levelBlockPos - 1].transform.position = pointsToMove[3].position;
            else
                blocks[_progressData.progressSave.levelBlockPos + 1].transform.position = pointsToMove[3].position;
        }
}
