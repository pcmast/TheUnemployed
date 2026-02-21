using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject gameCanvas;   // Canvas del juego
    public GameObject pauseCanvas;  // Canvas del menú de pausa

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                 PauseGame(); 
            }
              
          
                
        }
    }

    public void PauseGame()
    {
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }
}