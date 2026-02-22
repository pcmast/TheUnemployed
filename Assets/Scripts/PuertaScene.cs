using UnityEngine;
using UnityEngine.SceneManagement;
public class PuertaScene : MonoBehaviour
{
    public KeyCode teclaInteractuar = KeyCode.E;

    bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(teclaInteractuar))
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
