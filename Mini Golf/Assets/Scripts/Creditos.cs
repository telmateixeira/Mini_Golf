using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public GameObject canvasGameObject; // Arrastra aquí el Canvas en el Inspector
    public string nextSceneName; // Nombre de la escena a cargar

    public void ShowCanvas()
    {
        canvasGameObject.SetActive(true); // Muestra el Canvas
        StartCoroutine(HideCanvasAndLoadScene()); // Inicia la cuenta regresiva
    }

    private IEnumerator HideCanvasAndLoadScene()
    {
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        canvasGameObject.SetActive(false); // Oculta el Canvas
        SceneManager.LoadScene(nextSceneName); // Carga la siguiente escena
    }
}
