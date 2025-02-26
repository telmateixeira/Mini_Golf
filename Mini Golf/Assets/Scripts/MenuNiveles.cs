using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNiveles : MonoBehaviour
{
    public string nombreNivel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pelota"))
        {
            SceneManager.LoadSceneAsync(nombreNivel);
        }
    }
}
