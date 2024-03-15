using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }
    
    public static Action onLevelStart;
    public static Action onLevelLoaded;
    public static Action onLevelCompleted;
    public static Action onLevelFailed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        loadLevel();
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
        loadLevel();
    }

    public void nextLevel()
    {
        loadLevel();
    }

    public void loadLevel()
    {
        levelManager.Instance.initLevel();
        onLevelLoaded?.Invoke();
    }
}
