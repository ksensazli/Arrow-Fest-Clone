using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class Level : MonoBehaviour
{
    public SplineComputer splineComputer;

    [SerializeField] private GameObject _enemiesOnEnd;

    private void OnEnable()
    {
        _enemiesOnEnd.transform.position = splineComputer.EvaluatePosition(1f);
    }
}
