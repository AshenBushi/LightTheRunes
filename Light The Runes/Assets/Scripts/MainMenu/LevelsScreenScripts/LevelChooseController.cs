using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelChooseController : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;
    
    private const float Speed = 50f;
    private readonly Vector2 _target = new Vector2(10.8f, 0f), 
        _leftTarget = new Vector2(0f, 0f), 
        _rightTarget = new Vector2(21.6f, 0f),
        _safeTarget = new Vector2(0f, 25f);
    
        public GameObject[] blocks;
        public GameObject[] buttons;
        private bool _canMove = false;
        private bool _next = false;
        private Vector3 _preTarget;
        private float _timer = 0f;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
            blocks[_saveData.save.levelBlockPos].transform.position = _target;
        }

        private void FixedUpdate()
        {
            Move();
            switch (_saveData.save.levelBlockPos)
            {
                case 0:
                    buttons[0].SetActive(false);
                    buttons[1].SetActive(true);
                    break;
                case 1:
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(true);
                    break;
                case 2:
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(false);
                    break;
            }
            _timer -= Time.deltaTime;
        }

        public void NextBlock()
        {
            if (!(_timer <= 0)) return;
            if (_saveData.save.levelBlockPos >= 2) return;
            _saveData.save.levelBlockPos += 1;
            _next = true;
            _timer = 0.2f;
            StartMove(_rightTarget, _leftTarget);
        }

        public void PreBlock()
        {
            if (!(_timer <= 0)) return;
            if (_saveData.save.levelBlockPos <= 0) return;
            _saveData.save.levelBlockPos -= 1;
            _next = false;
            _timer = 0.2f;
            StartMove(_leftTarget, _rightTarget);
        }
    
        private void StartMove(Vector3 spawn, Vector3 preTarget)
        {
            _canMove = true;
            blocks[_saveData.save.levelBlockPos].transform.position = spawn;
            _preTarget = preTarget;
        }

        public void GoToLevel()
        {
            if (_saveData.save.energy > 0)
            {
                _saveData.save.energy --;
                _saveData.save.currentLevel = int.Parse(EventSystem.current.currentSelectedGameObject.name);
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

            Functions.MoveTo(_next ? blocks[_saveData.save.levelBlockPos - 1] : blocks[_saveData.save.levelBlockPos + 1], _preTarget, Speed);
            Functions.MoveTo(blocks[_saveData.save.levelBlockPos], _target, Speed);

            if (blocks[_saveData.save.levelBlockPos].transform.position != new Vector3(0f, 0f, 0f)) return;
            _canMove = false;
            if (_next)
                blocks[_saveData.save.levelBlockPos - 1].transform.position = _safeTarget;
            else
                blocks[_saveData.save.levelBlockPos + 1].transform.position = _safeTarget;
        }
}
