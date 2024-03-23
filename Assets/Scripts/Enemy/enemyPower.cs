using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck;
using UnityEngine;

public class enemyPower : MonoBehaviour
{
    public int _enemyPower;
    
    public void init(Transform enemy, int enemyPower)
    {
        transform.localScale = enemy.transform.localScale;
        transform.SetPositionAndRotation(enemy.transform.position, enemy.transform.rotation);
        _enemyPower = enemyPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            arrowController.Instance.arrowMinus(_enemyPower);
        }
    }
}
