using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyControllerCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody _hips;
    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody[] _rigidbodies;
    [Button]
    private void SetPreprocess()
    {
        var characterConstraints = GetComponentsInChildren<CharacterJoint>();
        foreach (var VAR in characterConstraints)
        {
            VAR.enablePreprocessing = true;
        }
    }

    [Button]
    private void SetRef()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetKinematic(true);
    }

    private void SetKinematic(bool state)
    {
        foreach (var VARIABLE in _rigidbodies)
        {
            VARIABLE.isKinematic = state;
            VARIABLE.useGravity = !state;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("OnTriggerEnter");
        if (other.CompareTag("arrow"))
        {
            AddForce();
        }
    }

    [Button]
    private void ResetRigidbodies()
    {
        _animator.enabled = true;
        foreach (var VARIABLE in _rigidbodies)
        {
            VARIABLE.velocity = Vector3.zero;
            VARIABLE.angularVelocity = Vector3.zero;
            SetKinematic(true);
        }
    }

    [Button]
    private void AddForce()
    {
        SetKinematic(false);
        _animator.enabled = false;
        _hips.AddForce(new Vector3(0,100,15) , ForceMode.Impulse);
        
    }
}
