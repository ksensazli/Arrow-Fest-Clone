using UnityEngine;

public class enemyOnEnd : enemyControllerCharacter
{
    [SerializeField] private Transform _parentObject;
    private void OnTriggerEnter(Collider other)
    {
        var enemyPower = transform.GetComponent<enemyPower>()._enemyPower;
        if (other.CompareTag("arrow"))
        {
            base.AddForce();
            for (int i = 0; i < enemyPower; i++)
            {
                GameObject arrowClone = objectPool.Instance.GetPooledObject(0);
                arrowClone.SetActive(true);
                arrowClone.transform.parent = _parentObject;
                //arrowClone.transform.position = transform.position + Vector3.up * 1.3f;
            }
        }
    }
}
