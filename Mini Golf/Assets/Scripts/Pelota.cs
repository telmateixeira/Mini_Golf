using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pelota : MonoBehaviour
{
    [Header("Refrencias")]
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject efectoMeta;

    [Header("Atributos")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;


    private bool isDraggin;
    private bool inHole;

    private void Update()
    {
        PlayerInput();

        if (LevelManager.main.outOfStrokes && rg.linearVelocity.magnitude <= 0.2f && !LevelManager.main.levelCompleted)
        {
            LevelManager.main.GameOver();
        }
    }


    private bool isReady() { 
        return rg.linearVelocity.magnitude <= 0.2f;
    }



    private void PlayerInput()
    {
        if (!isReady()) { return; }
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
        lr.SetPosition(1, (Vector2)transform.position - Vector2.ClampMagnitude((dir * power)/2, maxPower/2));
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

        LevelManager.main.IncreaseStroke();

        Vector2 dir = (Vector2) transform.position - pos;
        rg.linearVelocity = Vector2.ClampMagnitude(dir * power, maxPower);

    }

    private IEnumerator CheckWinState(Collider2D collision)
    {
        if (inHole) yield break; // Evita m�ltiples activaciones

        float holeRadius = collision.bounds.size.x / 2f;
        float ballRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        float distanceToCenter = Vector2.Distance(transform.position, collision.transform.position);

        // Si el centro de la pelota esta dentro del hoyo
        if (distanceToCenter < (holeRadius - ballRadius))
        {
            inHole = true;
            rg.linearVelocity = Vector2.zero; // Detener la pelota
            Debug.Log("Ganaste");

            LevelManager.main.levelCompleted = true;

            // Desactivar la c�mara antes de ocultar la pelota
            if (Camera.main.TryGetComponent<Camara>(out Camara camara))
            {
                camara.enabled = false;
            }

            yield return new WaitForSeconds(0.2f); // Esperar antes de ocultar la pelota
            gameObject.SetActive(false);

            GameObject fx = Instantiate(efectoMeta, transform.position, Quaternion.identity);
            Destroy(fx, 2f);

            LevelManager.main.LevelComplete();

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("hoyo"))
        {
            StartCoroutine(CheckWinState(collision));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("hoyo"))
        {
            StartCoroutine(CheckWinState(collision));
        }
    }
}
