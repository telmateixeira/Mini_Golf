using UnityEngine;


public class Camara : MonoBehaviour
{
    [SerializeField] private Transform target; // La pelota
    [SerializeField] private float smoothSpeed = 5f; // Velocidad de seguimiento
    [SerializeField] private Vector3 offset; // Offset para ajustar la posición de la cámara

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

