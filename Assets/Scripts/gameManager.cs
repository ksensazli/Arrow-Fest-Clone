using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }
}
