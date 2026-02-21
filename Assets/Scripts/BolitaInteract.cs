using UnityEngine;

public class BolitaInteract : MonoBehaviour
{
    public float interactionDistance = 13f; // distancia para recoger
    public AudioClip recogerSonido;
    private AudioSource audioSource;

    private static bool yaRecogidoEnEsteFrame = false; // bandera estática para evitar recoger varias

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (yaRecogidoEnEsteFrame) return; // otra bolita ya se recogió en este frame

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            yaRecogidoEnEsteFrame = true; // bloqueamos otras bolitas este frame
            RecogerBolita();
        }
    }

    void LateUpdate()
    {
        // Reset de la bandera para el siguiente frame
        yaRecogidoEnEsteFrame = false;
    }

    public void RecogerBolita()
    {
        if (Inventory.instance != null)
            Inventory.instance.AddPaperBall();

        if (recogerSonido != null)
        {
            audioSource.PlayOneShot(recogerSonido);
            Destroy(gameObject, recogerSonido.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}