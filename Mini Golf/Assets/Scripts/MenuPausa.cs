using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPanel; // Asigna el Panel en el Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);
    }
}
