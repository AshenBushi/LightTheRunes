using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform mainCamera; //Camera
    private bool _canCameraMove = false;
    private Vector3 _targetToMove;
    private const float Speed = 70f;
    private int _cameraCheckPos = 0;
    private float _timer;
    
    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!(_timer <= 0)) return;
        if (!((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))) return;
        if (eventData.delta.x > 0)
        {
            if (_cameraCheckPos <= (-1)) return;
            var position = mainCamera.transform.position;
            StartCameraMove(new Vector3(position.x - 10.8f, position.y, position.z));
            _cameraCheckPos -= 1;
            _timer = 0.2f;
        }
        else
        {
            if (_cameraCheckPos >= (1)) return;
            var position = mainCamera.transform.position;
            StartCameraMove(new Vector3(position.x + 10.8f, position.y, position.z));
            _cameraCheckPos += 1;
            _timer = 0.2f;
        }
    }

    private void FixedUpdate()
    {
        Move();
        _timer -= Time.deltaTime;
    }

    private void StartCameraMove(Vector3 target)
    {
        _targetToMove = target;
        _canCameraMove = true;
    }

    private void Move()
    {
        if (_canCameraMove)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, _targetToMove, Speed * Time.deltaTime);
        }
        if (mainCamera.transform.position == _targetToMove)
            _canCameraMove = false;
    }
}
