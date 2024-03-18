using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

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

    public void addObjectToPool(int poolIndex)
    {
        GameObject objectClone = Instantiate(Pools[poolIndex].objectToPool);
        objectClone.SetActive(false);
        objectClone.transform.SetParent(transform);
        Pools[poolIndex].pooledObject.Add(objectClone);
    }
    
    public GameObject GetPooledObject(int obj)
    {
        for(int i = 0; i < Pools[obj].pooledObject.Count; i++)
        {
            if(!Pools[obj].pooledObject[i].activeInHierarchy)
            {
                return Pools[obj].pooledObject[i];
            }
        }
        addObjectToPool(obj);
        return Pools[obj].pooledObject[Pools[obj].pooledObject.Count - 1];
    }

    public void returnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
    }
}
