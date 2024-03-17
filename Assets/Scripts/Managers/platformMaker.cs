using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class platformMaker : MonoBehaviour
{
    [SerializeField] private SplineMesh _splineMesh;
    [SerializeField] private TubeGenerator _tubeGeneratorLeft;
    [SerializeField] private TubeGenerator _tubeGeneratorRight;
    [SerializeField] private GameObject _endPath;
    [SerializeField] private Transform _endLineCollider;

    private void Update()
    {
        _splineMesh.spline = levelManager.Instance.level.splineComputer;
        _tubeGeneratorLeft.spline = levelManager.Instance.level.splineComputer;
        _tubeGeneratorRight.spline = levelManager.Instance.level.splineComputer;
        _endPath.SetActive(true);
        _endPath.transform.position = _splineMesh.EvaluatePosition(1f) + 12.85f * Vector3.forward;
        _endLineCollider.localPosition = _splineMesh.EvaluatePosition(1f) - 0.5f * Vector3.forward;
    }
}
