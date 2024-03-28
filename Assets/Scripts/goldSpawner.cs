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
            goldClone.GetComponent<gold>().init(_goldObjects[i].transform,1);
        }
    }
}
