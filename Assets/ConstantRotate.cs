using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public Vector3 RotationalAxis;

    public float RotationSpeed;

    void Update()
    {
        transform.Rotate(RotationSpeed * RotationalAxis, Space.Self);
    }
}
