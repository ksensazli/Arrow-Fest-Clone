using System;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }
    
    public static Action onLevelStart;
    public static Action onLevelLoaded;
    public static Action onLevelCompleted;
    public static Action onLevelFailed;
    
    private bool _isStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        inputManager.onInputDown += OnInputDown;
    }

    private void OnDisable()
    {
        inputManager.onInputDown -= OnInputDown;
    }

    private void OnInputDown()
    {
        if (_isStart)
        {
            return;
        }
        //startLevel();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        DG.Tweening.DOTween.SetTweensCapacity(500, 50);
        loadLevel();
    }

    public void startLevel()
    {
        onLevelStart?.Invoke();
        _isStart = true;
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
        _isStart = false;
        levelManager.Instance.initLevel();
        onLevelLoaded?.Invoke();
    }
}
