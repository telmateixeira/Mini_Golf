using UnityEngine;

public class TroncoMovimiento : MonoBehaviour
{
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;
    [SerializeField] private float velocidad = 2f;

    private Vector3 destino;
    private Vector3 direccion;

    private void Start()
    {
        destino = puntoB.position;
        direccion = (destino - transform.position).normalized; // Direcci�n inicial
    }

    private void Update()
    {
        // Mueve el objeto en la direcci�n establecida
        transform.position += direccion * velocidad * Time.deltaTime;

        // Si el Tilemap llega a su destino, cambia la direcci�n
        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = (destino == puntoA.position) ? puntoB.position : puntoA.position;
            direccion = (destino - transform.position).normalized; // Recalcula la direcci�n
        }
    }


}
