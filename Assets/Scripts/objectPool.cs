using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class objectPool : MonoBehaviour
{
    public static objectPool Instance { get; private set; }
    
    [SerializeField] private GameObject _objectToPool;
    private int _amountToPool = 100;
    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for(int i = 0; i < _amountToPool; i++)
        {
            GameObject objectClone = Instantiate(_objectToPool);
            objectClone.SetActive(false);
            objectClone.transform.SetParent(transform);
            pooledObjects.Add(objectClone);
        }
    }
    
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < _amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void returnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
    }
}
