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
    private int _highScore;

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
    }
    
    public void completeLevel()
    {
        onLevelCompleted?.Invoke();
    }

    public void failedLevel()
    {
        onLevelFailed?.Invoke();
    }

    public void restartLevel()
    {
        levelManager.Instance.initLevel();
    }

    public void nextLevel()
    {
        _highScore = GameConfig.Instance.levelNum++;
        levelManager.Instance.initLevel();
    }
}
