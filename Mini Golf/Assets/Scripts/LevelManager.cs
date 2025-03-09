using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelCompletedStrokeUI;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;
    [Header("Attributes")]
    [SerializeField] private int maxStrokes;
    private int strokes = 0;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateStrokeUI();
    }

    public void IncreaseStroke()
    {
        strokes++;
        UpdateStrokeUI();
        if (strokes >= maxStrokes && !levelCompleted)
        {
            outOfStrokes = true;
        }
    }

    public void LevelComplete()
    {
        levelCompleted = true;

        levelCompletedStrokeUI.text = strokes > 1
            ? "Has completado el nivel en " + strokes + " tiros."
            : "Hoyo en uno!";

        levelCompleteUI.SetActive(true);
    }

    public void GameOver()
    {
        if (levelCompleted) return; // Evita que se muestre Game Over si ya ganó
        gameOverUI.SetActive(true);
    }


    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }
}

