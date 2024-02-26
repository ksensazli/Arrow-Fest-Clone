using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static Action onLevelStart;
    public static Action onLevelCompleted;
    public static Action onEndLevel;

    private bool _isStart;
    private bool _isComplete;
    private bool _isEnd;
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }

    private void startLevel()
    {
        onLevelStart?.Invoke();
        _isStart = true;
    }
    
    private void completeLevel()
    {
        onLevelCompleted?.Invoke();
        _isComplete = true;
    }

    private void endLevel()
    {
        onEndLevel?.Invoke();
        _isEnd = true;
    }

    private void Update()
    {
        if (!_isStart && Input.GetMouseButtonDown(0))
        {
            startLevel();
            return;
        }
        
        if(!_isEnd || !_isComplete)
            return;
    }
}
