using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseButtons : MonoBehaviour
{
    private SessionData _sessionData;
    private ProgressData _progressData;
    private Functions _functions;
    
    private void Start()
    {
        _sessionData = FindObjectOfType<SessionData>();
        _progressData = FindObjectOfType<ProgressData>();
        _functions = FindObjectOfType<Functions>();
    }

    public void Home()
    {
        _functions.ToScene("MainMenu");
    }

    public void Restart()
    {
        if (_progressData.progressSave.energy <= 0)
            _functions.EmptyEnergy();
        else
        {
            _progressData.progressSave.energySave--;
            _functions.ToScene("Gameplay");
        }
    }
}
