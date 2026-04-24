using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform vrCamera;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float height = 0f;

    private void LateUpdate()
    {
        if (vrCamera == null) return;

        transform.position = vrCamera.position + vrCamera.forward * distance + Vector3.up * height;
        transform.rotation = vrCamera.rotation;
    }
}