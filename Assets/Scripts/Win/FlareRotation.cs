using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareRotation : MonoBehaviour
{
    public Transform flare;
    private void FixedUpdate()
    {
        flare.Rotate(0f, 0f, -0.5f, Space.Self);
    }
}
