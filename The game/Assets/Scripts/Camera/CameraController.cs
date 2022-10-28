using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain virtualCamera;
    public float bottomBoundary = -1.4f;

    private void Awake() => virtualCamera = GetComponent<CinemachineBrain>();

    void Update()
    {
        if (transform.position.y < bottomBoundary)
            virtualCamera.enabled = false;
    }
}
