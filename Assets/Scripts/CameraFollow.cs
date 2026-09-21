using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Настройки камеры")]
    [SerializeField] private Transform target;    
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -10f);
    [SerializeField] private float smoothSpeed = 10f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}