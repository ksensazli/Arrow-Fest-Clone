using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _goldObjects;

    private void OnEnable()
    {
        for (int i = 0; i < _goldObjects.Length; i++)
        {
            GameObject goldClone = objectPool.Instance.GetPooledObject(1);
            goldClone.SetActive(true);
            goldClone.transform.position = _goldObjects[i].transform.position;
            goldClone.transform.localScale = _goldObjects[i].transform.localScale;
        }
    }
}
