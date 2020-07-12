using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraSwipe : MonoBehaviour
{
    private SaveData _saveData;
    private Functions _functions;
    
    public Transform mainCamera; //Camera
    public Transform activeScreen;
    public Sprite toMove; 
    public Sprite toStay;
    private bool _canCameraMove = false, _activeScreenCanMove = false;
    private Vector3 _targetToMove, _activeTargetToMove;
    private const float Speed = 70f;
    private float _correctSpeed;
    private int _cameraCheckPos = 0;
    private float _timer;
    private Image _spriteRenderer;
    
    private void Start()
    {
        _saveData = FindObjectOfType<SaveData>();
        _functions = FindObjectOfType<Functions>();
        _spriteRenderer = activeScreen.GetComponent<Image>();
    }
    private void FixedUpdate() 
    { 
        Move();
            
        if(_timer > 0)
            _timer -= Time.deltaTime;
    }

    private void StartCameraMove(Vector3 target, Vector3 activeTarget, float correct)
    { 
        _correctSpeed = correct;
        _targetToMove = target;
        _activeTargetToMove = activeTarget;
        _canCameraMove = true;
        activeScreen.GetComponent<Image>().sprite = toMove;
        _activeScreenCanMove = true;
    }

    private void Move()
    {
        if (_canCameraMove)
        { 
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, _targetToMove, Speed * Time.deltaTime);
        }
        if (mainCamera.transform.position == _targetToMove)
            _canCameraMove = false;
        
        if (_activeScreenCanMove)
        {
            activeScreen.transform.position = Vector3.MoveTowards(activeScreen.transform.position, _activeTargetToMove, Speed * _correctSpeed * Time.deltaTime);
        }

        if (activeScreen.position != _activeTargetToMove) return;
        _spriteRenderer.sprite = toStay;
        _activeScreenCanMove = false;
    }

    public void ButtonControl(int butNumber)
    {
        if (!(_timer <= 0)) return;
        var position = mainCamera.transform.position;
        var activePosition = activeScreen.position;

        switch (butNumber)
        {
            case 1:
                if (_cameraCheckPos != -1)
                {
                    if (_cameraCheckPos == 0)
                        StartCameraMove(new Vector3(position.x - 10.8f, position.y, position.z), new Vector3(activePosition.x - 2.157f * 2.95f, activePosition.y, activePosition.z), 0.5157f);
                    else
                        StartCameraMove(new Vector3(position.x - 21.6f, position.y, position.z), new Vector3(activePosition.x - 2.157f * 2.95f * 2f, activePosition.y, activePosition.z), 0.5157f);
                    _cameraCheckPos = -1;
                    _timer = 0.3f;
                }
                break;
            case 2:
                if (_cameraCheckPos != 0)
                {
                    if (_cameraCheckPos == -1) 
                        StartCameraMove(new Vector3(position.x + 10.8f, position.y, position.z), new Vector3(activePosition.x + 2.157f * 2.95f, activePosition.y, activePosition.z), 0.5157f);
                    else
                        StartCameraMove(new Vector3(position.x - 10.8f, position.y, position.z), new Vector3(activePosition.x - 2.157f * 2.95f, activePosition.y, activePosition.z), 0.5157f);
                    _cameraCheckPos = 0;
                    _timer = 0.3f;
                }
                break;
            case 3:
                if (_cameraCheckPos != 1)
                {
                    if (_cameraCheckPos == 0)
                        StartCameraMove(new Vector3(position.x + 10.8f, position.y, position.z), new Vector3(activePosition.x + 2.157f * 2.95f, activePosition.y, activePosition.z), 0.5157f);
                    else
                        StartCameraMove(new Vector3(position.x + 21.6f, position.y, position.z), new Vector3(activePosition.x + 2.157f * 2.95f * 2f, activePosition.y, activePosition.z), 0.5157f);
                    _cameraCheckPos = 1;
                    _timer = 0.3f;
                } 
                break;
        }
    }
}
