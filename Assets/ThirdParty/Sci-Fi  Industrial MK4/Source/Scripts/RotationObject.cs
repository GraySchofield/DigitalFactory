using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private Vector3 Rotation;
    [SerializeField] private float Speed = 10f;

    private void Update()
    {
        transform.Rotate(Rotation, Time.deltaTime * Speed);
    }
}