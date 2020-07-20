using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoStarsStart : MonoBehaviour
{
    public GameObject[] logoStars;

    private void Start()
    {
        StartCoroutine(StartAnim());
    }

    private IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.7f);
        logoStars[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        logoStars[0].SetActive(true);
        yield return new WaitForSeconds(0.8f);
        logoStars[2].SetActive(true);
    }
}
