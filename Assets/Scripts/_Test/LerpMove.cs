using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _lerp;
    [SerializeField] private Transform _moveTowards;
    [SerializeField] private float _moveTowardSpeed;

    private Vector3 _targetPos;
    private void Update()
    {
        _lerp.position = Vector3.Lerp(_lerp.position, _targetPos, Time.deltaTime * 2);
        _moveTowards.position = Vector3.MoveTowards(_moveTowards.position, _targetPos, Time.deltaTime * _moveTowardSpeed);
        if (inputManager.Instance.IsInputDown)
        {
            _targetPos = new Vector3(10, 0, 0);
            return;
        }
      
    }
}
