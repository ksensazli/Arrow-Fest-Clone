using UnityEngine;


public class SimpleRotator : MonoBehaviour
{
    public float Speed;
    public Vector3 RotationVector;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(RotationVector * Speed * Time.deltaTime);
    }
}