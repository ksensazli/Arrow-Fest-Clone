using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    
    public static gameManager Instance { get; private set; }
    
    public static Action onLevelStart;
    public static Action onLevelCompleted;
    public static Action onLevelFailed;

    //private bool _isStart;
    //private bool _isComplete;
    //private bool _isFailed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 120;
    }

    public void startLevel()
    {
        onLevelStart?.Invoke();
        //_isStart = true;
    }
    
    public void completeLevel()
    {
        onLevelCompleted?.Invoke();
    }

    public void failedLevel()
    {
        onLevelFailed?.Invoke();
        //_isFailed = true;
        Debug.LogError("Failed");
    }

    public void restartLevel()
    {
        levelManager.Instance.clearLevel();
        levelManager.Instance.initLevel();
    }

    public void nextLevel()
    {
        levelManager.Instance.clearLevel();
        levelManager.Instance.nextLevel();
    }
}
