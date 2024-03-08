using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck;
using UnityEngine;

public class enemyPower : MonoBehaviour
{
    [SerializeField] private int _enemyPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            arrowController.Instance.arrowMinus(_enemyPower);
        }
    }
}
