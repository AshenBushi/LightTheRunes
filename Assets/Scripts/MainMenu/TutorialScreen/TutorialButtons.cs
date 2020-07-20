using System;
using System.Collections;
using UnityEngine;

namespace MainMenu.TutorialScreen
{
    public class TutorialButtons : MonoBehaviour
    {
        private SessionData _sessionData;
        
        public GameObject firstSlideButton;
        public GameObject thirdSlideButton;
        public GameObject[] slides;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
        }

        private void Update()
        {
            if (_sessionData.sessionSave.cameraPos != 0)
            {
                slides[0].SetActive(true);
                for (var i = 1; i <= 4; i++)
                    slides[i].SetActive(false);
            }
        }

        public void FirstSlideButton()
        {
            StartCoroutine(FirstSlide());
        }
        
        public void ThirdSlideButton()
        {
            StartCoroutine(ThirdSlide());
        }

        private IEnumerator FirstSlide()
        {
            slides[0].SetActive(false);
            slides[1].SetActive(true);
            yield return new WaitForSeconds(2f);
            slides[1].SetActive(false);
            slides[2].SetActive(true);
        }
        
        private IEnumerator ThirdSlide()
        {
            slides[2].SetActive(false);
            slides[3].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            slides[3].SetActive(false);
            slides[4].SetActive(true);
            yield return new WaitForSeconds(2.5f);
            FindObjectOfType<ScreensMove>().NavigationButton(2);
        }
        
    }
}
