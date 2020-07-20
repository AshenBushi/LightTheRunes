using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainMenu
{
    public class ScreensMove : MonoBehaviour
    {
        private SessionData _sessionData;
        private Functions _functions;

        public float speed;

        public GameObject[] screens;
        public Image[] navigationButtons;
        public Sprite[] navigationSpritesOn;
        public Sprite[] navigationSpritesOff;
        public Transform[] pointsToMove;
        public bool canMove = false;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();

            for (var i = 0; i < 5; i++)
                screens[i].transform.position = pointsToMove[i + 4 - _sessionData.sessionSave.cameraPos].position;
        }

        private void Update()
        {
            Move();
            for(var i = 0; i < 5; i++) 
                navigationButtons[i].sprite = _sessionData.sessionSave.cameraPos == i ? navigationSpritesOn[i] : navigationSpritesOff[i];
        }

        public void NavigationButton(int Pos)
        {
            speed = 40f;
            for (var i = 0; i < Math.Abs(_sessionData.sessionSave.cameraPos - Pos); i++)
                speed += 20f;
            _sessionData.sessionSave.cameraPos = Pos;
            canMove = true;
        }
        
        private void Move()
        {
            if (!canMove) return;

            Functions.MoveTo(screens[0], pointsToMove[4 - _sessionData.sessionSave.cameraPos].position, speed);
            Functions.MoveTo(screens[1], pointsToMove[5 - _sessionData.sessionSave.cameraPos].position, speed);
            Functions.MoveTo(screens[2], pointsToMove[6 - _sessionData.sessionSave.cameraPos].position, speed);
            Functions.MoveTo(screens[3], pointsToMove[7 - _sessionData.sessionSave.cameraPos].position, speed);
            Functions.MoveTo(screens[4], pointsToMove[8 - _sessionData.sessionSave.cameraPos].position, speed);
            if (screens[_sessionData.sessionSave.cameraPos].transform.position == pointsToMove[4].position)
                canMove = false;
        }
    }
}
