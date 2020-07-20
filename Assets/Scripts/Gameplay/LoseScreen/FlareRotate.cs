using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlareRotate : MonoBehaviour
{
    public Transform flare;
    private void FixedUpdate()
    {
        flare.Rotate(0f, 0f, -0.5f, Space.Self);
    }
}
