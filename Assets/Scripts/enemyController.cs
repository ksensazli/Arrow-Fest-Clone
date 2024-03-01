using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private int _enemyPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            //An animated escape animation or script can be added here.
            transform.DOMoveY(50, 2f, false);
            transform.DOScale(0.001f, 1f).OnComplete(() =>
            {
                transform.gameObject.SetActive(false);
            });
            arrowController.Instance.arrowMinus(_enemyPower);
        }
    }
}
