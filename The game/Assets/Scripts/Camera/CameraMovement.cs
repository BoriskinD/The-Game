using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset = new (0, 10, -10);

    void LateUpdate() => transform.position = target.transform.position + offset;
}
