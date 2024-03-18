using System;
using System.Collections;
using Dreamteck.Splines;
using UnityEngine;

public class platformMaker : MonoBehaviour
{
    [SerializeField] private SplineMesh _splineMesh;
    [SerializeField] private TubeGenerator _tubeGeneratorLeft;
    [SerializeField] private TubeGenerator _tubeGeneratorRight;
    [SerializeField] private GameObject _endPath;

    private void OnEnable()
    {
        gameManager.onLevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        gameManager.onLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        _splineMesh.spline = levelManager.Instance.level.splineComputer;
        _tubeGeneratorLeft.spline = levelManager.Instance.level.splineComputer;
        _tubeGeneratorRight.spline = levelManager.Instance.level.splineComputer;
        StartCoroutine(loadEndPath());
    }

    private IEnumerator loadEndPath()
    {
        yield return new WaitForEndOfFrame();
        _endPath.SetActive(true);
        _endPath.transform.position = _splineMesh.EvaluatePosition(1f) + 12.85f * Vector3.forward;
        //ROTATION
        //levelManager.Instance.level.splineComputer.GetPoint...()
    }
}
