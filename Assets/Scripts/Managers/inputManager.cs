using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public static inputManager Instance { get; private set; }
    public static Action onInputDown;

    public bool IsInputDown => _isInputDown;
    private bool _isInputDown;
    
    private float _lastPosX;
    private float _dragAmountX;
    public float DragAmountX => _dragAmountX;
    private float _dragAmountDelta;
    public float DragAmountDeltaX => _dragAmountDelta;
    private float _startPosX;
    private float _offset;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isInputDown = true;
            onInputDown?.Invoke();
            _startPosX = Input.mousePosition.x;
            _lastPosX = _startPosX;
        }
        else if (Input.GetMouseButton(0))
        {
            _isInputDown = true;
            _dragAmountX = Input.mousePosition.x - _lastPosX;
            _lastPosX =  Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isInputDown = false;
            _dragAmountDelta = 0;
        }
    }
}
