using Boo.Lang;
using UnityEngine;

namespace MainMenu.LevelsScreenScripts
{
    public class MovementLevelBlocks : MonoBehaviour
    {
        private SessionData _sessionData;

        [SerializeField] private GameObject[] blocks;
        [SerializeField] private Transform[] pointsToMove;

        private int _currentBlock;
    
        private float _timer;
        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();

            _currentBlock = 0;
            blocks[_currentBlock].transform.position = pointsToMove[1].position;
        }

        private bool _canMoving;
        private void Update()
        {
            if (_canMoving)
                Moving();
            DetectSwipe();
            _timer -= Time.deltaTime;
        }

        private Vector2 _mouseButtonDownPosition;
        private Vector2 _mouseButtonUpPosition;
    
        private void DetectSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseButtonDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _mouseButtonUpPosition = Input.mousePosition;
                
                CheckDirection(_mouseButtonDownPosition, _mouseButtonUpPosition);
            }
        }
    
        private void CheckDirection(Vector2 mouseButtonDownPosition, Vector2 mouseButtonUpPosition)
        {
            if (mouseButtonDownPosition == mouseButtonUpPosition || _sessionData.sessionSave.cameraPos != 3 || _sessionData.sessionSave.pause) return;
            
            if (mouseButtonUpPosition.x < mouseButtonDownPosition.x)
            {
                RightSwipe();
            }

            if (mouseButtonUpPosition.x > mouseButtonDownPosition.x)
            {
                LeftSwipe();
            }
        }

        private string _direction;
    
        private void LeftSwipe()
        {
            if (_timer > 0) return;
            if (_currentBlock == 0) return;
            _currentBlock -= 1;
            _direction = "left";
            _timer = 0.2f;
            StartMoving(pointsToMove[0].position, pointsToMove[2].position);
        }

        private void RightSwipe()
        {
            if (_timer > 0) return;
            if (_currentBlock == 1) return;
            _currentBlock += 1;
            _direction = "right";
            _timer = 0.2f;
            StartMoving(pointsToMove[2].position, pointsToMove[0].position);
        }

        private Vector3 _blockToSavePosition;
        private void StartMoving(Vector3 spawn, Vector3 pointToSave)
        {
            _canMoving = true;
            blocks[_currentBlock].transform.position = spawn;
            _blockToSavePosition = pointToSave;
        }

        private const float Speed = 50f;
        private void Moving()
        {
            Functions.MoveTo(_direction == "right" ? blocks[0] : blocks[1], _blockToSavePosition, Speed);
            Functions.MoveTo(blocks[_currentBlock], pointsToMove[1].position, Speed);

            if (blocks[_currentBlock].transform.position != pointsToMove[1].position) return;
            _canMoving = false;

            switch (_direction)
            {
                case "right":
                    blocks[0].transform.position = pointsToMove[3].position;
                    break;
                case "left":
                    blocks[1].transform.position = pointsToMove[3].position;
                    break;
                default:
                    break;
            }    
        }
    }
}
