using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    public GameObject objectToPool;
    public int amountToPool;
    public List<GameObject> pooledObject = new List<GameObject>();
}
public class objectPool : MonoBehaviour
{
    public static objectPool Instance { get; private set; }
    
    public List<Pool> Pools = new List<Pool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < Pools.Count; i++)
        {
            for(int j = 0; j < Pools[i].amountToPool; j++)
            {
                addObjectToPool(i);
            }
        }
    }

    private void OnEnable()
    {
        gameManager.onLevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        gameManager.onLevelCompleted -= OnLevelCompleted;
    }

    public void addObjectToPool(int poolIndex)
    {
        GameObject objectClone = Instantiate(Pools[poolIndex].objectToPool);
        objectClone.SetActive(false);
        objectClone.transform.SetParent(transform);
        Pools[poolIndex].pooledObject.Add(objectClone);
    }
    
    public GameObject GetPooledObject(int poolIndex)
    {
        for(int i = 0; i < Pools[poolIndex].pooledObject.Count; i++)
        {
            if(!Pools[poolIndex].pooledObject[i].activeInHierarchy)
            {
                return Pools[poolIndex].pooledObject[i];
            }
        }
        addObjectToPool(poolIndex);
        return Pools[poolIndex].pooledObject[Pools[poolIndex].pooledObject.Count - 1];
    }

    public void returnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        obj.transform.rotation = Quaternion.Euler(90,0,0);
        obj.transform.position = Vector3.zero;
    }

    private void OnLevelCompleted()
    {
        Debug.Log("On Level Completed!");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < Pools[i].pooledObject.Count; j++)
            {
                if(!Pools[i].pooledObject[j].activeInHierarchy)
                {
                    returnToPool(Pools[i].pooledObject[j]);
                }
            }
        }
    }
}
