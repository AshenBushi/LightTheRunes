using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCheck : MonoBehaviour
{
    public Sprite x4On;
    public Sprite x5On;
    private void Start()
    {
        StarsSavingSystem.Edit(19, 1);
        if (gameObject.name == "Play4x4" && StarsSavingSystem.Get(19) >= 1)
        {
            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<Image>().sprite = x4On;
        }

        if (gameObject.name != "Play5x5" || StarsSavingSystem.Get(40) < 1) return;
        gameObject.GetComponent<Button>().interactable = true;
        gameObject.GetComponent<Image>().sprite = x5On;
    }
}
