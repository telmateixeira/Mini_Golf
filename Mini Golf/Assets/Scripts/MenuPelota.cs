using System.Collections;
using UnityEngine;

public class MenuPelota : MonoBehaviour
{
    [Header("Refrencias")]
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject efectoMeta;

    [Header("Atributos")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;

    private bool isDraggin;

    private void Update()
    {
        PlayerInput();
    }


    private void PlayerInput()
    {
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f)
        {
            DragStart();
        }
        if (Input.GetMouseButton(0) && isDraggin)
        {
            DragChange(inputPos);
        }
        if (Input.GetMouseButtonUp(0) && isDraggin)
        {
            DragRelease(inputPos);
        }
    }

    private void DragStart()
    {
        isDraggin = true;
        lr.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position - Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDraggin = false;
        lr.positionCount = 0;

        if (distance < 1f)
        {
            return;
        }

        Vector2 dir = (Vector2)transform.position - pos;
        rg.linearVelocity = Vector2.ClampMagnitude(dir * power, maxPower);

    }

    private void CheckWinState()
    {
        rg.linearVelocity = Vector2.zero; // Detener la pelota

        // Desactivar la camara antes de ocultar la pelota
        //if (Camera.main.TryGetComponent<Camara>(out Camara camara))
        //{
        //    camara.enabled = false;
        //}

        //yield return new WaitForSeconds(0.2f); // Esperar antes de ocultar la pelota
        gameObject.SetActive(false);

        GameObject fx = Instantiate(efectoMeta, transform.position, Quaternion.identity);
        Destroy(fx, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("level"))
        {
            CheckWinState();
        }
    }


}
