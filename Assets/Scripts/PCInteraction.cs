using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    public Transform player;
    public CharacterController playerController;
    public Camera playerCamera;
    public Camera pcCamera;
    public float interactDistance = 2f;
    public KeyCode interactKey = KeyCode.E;

    private bool interacting = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!interacting && distance <= interactDistance && Input.GetKeyDown(interactKey))
        {
            StartInteraction();
        }
        else if (interacting && Input.GetKeyDown(interactKey))
        {
            EndInteraction();
        }
    }

    void StartInteraction()
    {
        interacting = true;
        playerController.enabled = false;
        playerCamera.enabled = false;
        pcCamera.enabled = true;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void EndInteraction()
    {
        interacting = false;
        playerController.enabled = true;
        playerCamera.enabled = true;
        pcCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

