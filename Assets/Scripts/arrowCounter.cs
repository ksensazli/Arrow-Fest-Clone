using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class arrowCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _arrowCount;
    private int _arrowAmount;

    private void OnEnable()
    {
        _arrowAmount = arrowController.Instance.arrowCount;
        _arrowCount.text = _arrowAmount.ToString();
    }

    private void Update()
    {
        _arrowAmount = arrowController.Instance.arrowCount;
        _arrowCount.text = _arrowAmount.ToString();
        DOTween.Kill(_arrowCount.transform);
        _arrowCount.transform.localScale = Vector3.one;
        _arrowCount.transform.DOPunchScale(Vector3.one * .25f, .15f)
            .OnComplete(()=>_arrowCount.transform.localScale = Vector3.one);
        _arrowAmount = arrowController.Instance.arrowCount;
    }
}
