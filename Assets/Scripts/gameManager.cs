using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private int _targetFPS;
    private void OnEnable()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
