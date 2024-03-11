using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck;
using UnityEngine;

public class enemyPower : MonoBehaviour
{
    public int _enemyPower;

    public static enemyPower Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            arrowController.Instance.arrowMinus(_enemyPower);
        }
    }
}
