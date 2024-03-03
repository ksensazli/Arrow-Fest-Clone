using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private int _enemyPower;
    [SerializeField] private Collider _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            _collider.enabled = false;
            //An animated escape animation or script can be added here.
            transform.DOMoveY(50, 2f);
            transform.DOScale(0, 1f).OnComplete(() =>
            {
                transform.gameObject.SetActive(false);
            });
            arrowController.Instance.arrowMinus(_enemyPower);
        }
    }
}
