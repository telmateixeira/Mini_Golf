using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("El nombre de la escena no puede estar vacío.");
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Si estamos en una build del juego
                Application.Quit();
        #endif
    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
