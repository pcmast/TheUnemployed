using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public GameObject canvasPrincipal;
    public GameObject panelOpciones;

    public void atras()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(3);
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
      public void AbrirPanel()
    {
        panelOpciones.SetActive(true);
    }
    public void CerrarPanel()
    {
        panelOpciones.SetActive(false);
        canvasPrincipal.SetActive(true);
    }
}