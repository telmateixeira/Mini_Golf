using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pelota : MonoBehaviour
{
    [Header("Refrencias")]
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private LineRenderer lr;

    [Header("Atributos")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDraggin;
    private bool inHole;

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
            DragChange();
        }
        if (Input.GetMouseButtonUp(0) && isDraggin)
        {
            DragRelease(inputPos);
        }
    }

    private void DragStart()
    {
        isDraggin = true;
    }

    private void DragChange()
    {

    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDraggin = false;

        if (distance < 1f) 
        {
            return;
        }

        Vector2 dir = (Vector2) transform.position - pos;
        rg.linearVelocity = Vector2.ClampMagnitude(dir * power, maxPower);

    }
}
