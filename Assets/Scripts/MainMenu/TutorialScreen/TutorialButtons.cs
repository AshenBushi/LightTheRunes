using System;
using System.Collections;
using UnityEngine;

namespace MainMenu.TutorialScreen
{
    public class TutorialButtons : MonoBehaviour
    {
        public GameObject[] slides;

        public void SlideButton(int num)
        {
            switch (num)
            {
                case 0:
                    slides[0].SetActive(false);
                    slides[1].SetActive(true);
                    break;
                case 1:
                    slides[1].SetActive(false);
                    slides[2].SetActive(true);
                    break;
                case 2:
                    slides[2].SetActive(false);
                    slides[3].SetActive(true);
                    break;
                case 3:
                    slides[3].SetActive(false);
                    slides[4].SetActive(true);
                    break;
                case 4:
                    slides[4].SetActive(false);
                    slides[5].SetActive(true);
                    StartCoroutine(EndSlide());
                    break;
            }
        }

        private IEnumerator EndSlide()
        {
            yield return new WaitForSeconds(4f);
            FindObjectOfType<ScreensMove>().NavigationButton(2);
            slides[0].SetActive(true);
            for (var i = 1; i <= 5; i++)
                slides[i].SetActive(false);
        }
    }
}
