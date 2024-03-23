using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyObjects;
    
    private void OnEnable()
    {
        for (int i = 0; i < _enemyObjects.Length; i++)
        {
            GameObject enemyClone = objectPool.Instance.GetPooledObject(2);
            enemyClone.SetActive(true);
            enemyClone.GetComponent<enemyPower>().init(_enemyObjects[i].transform,10);
        }
    }
}
