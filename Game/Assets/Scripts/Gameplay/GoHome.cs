using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
