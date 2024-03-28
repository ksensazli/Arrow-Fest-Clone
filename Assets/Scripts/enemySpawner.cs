using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyObjects;
    private GameObject[] _enemyToReset;
    
    private void OnEnable()
    {
        for (int i = 0; i < _enemyObjects.Length; i++)
        {
            GameObject enemyClone = objectPool.Instance.GetPooledObject(2);
            enemyClone.SetActive(true);
            enemyClone.GetComponent<enemyPower>().init(_enemyObjects[i].transform,10);
        }
    }

    private void OnDisable()
    {
        _enemyToReset = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < _enemyObjects.Length; i++)
        {
            _enemyToReset[i].GetComponent<enemyControllerCharacter>().ResetRigidbodies();
            objectPool.Instance.returnToPool(_enemyToReset[i]);
        }
    }
}
