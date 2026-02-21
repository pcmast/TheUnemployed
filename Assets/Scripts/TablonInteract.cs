using UnityEngine;

public class TablonInteract : MonoBehaviour
{
    public GameObject canvasTareas;// El Canvas que se abrirá
    public float interactionDistance = 3f;// Distancia máxima para interactuar

    private bool isOpen = false;
    private Transform player;

    void Start()
    {
        // Busca automáticamente al jugador con el tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con tag 'Player'");
        }

        // Aseguramos que el canvas empieza cerrado
        if (canvasTareas != null)
            canvasTareas.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        // Calculamos la distancia
        float distance = Vector3.Distance(player.position, transform.position);
        // Si estamos cerca y presionamos E
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            ToggleCanvas();
        }
    }

    void ToggleCanvas()
    {
        isOpen = !isOpen;

        if (canvasTareas != null)
            canvasTareas.SetActive(isOpen);

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // pausa el juego
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f; // reanuda el juego
        }
    }
}