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
        _endLineCollider.localPosition = new Vector3(_endPath.transform.localPosition.x,
            _endPath.transform.localPosition.y, _endPath.transform.localPosition.z - 13.35f);
    }
}
