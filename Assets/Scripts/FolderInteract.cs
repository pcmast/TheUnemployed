using UnityEngine;

public class FolderInteract : MonoBehaviour
{
    public float interactionDistance = 13f; // distancia para recoger
    public AudioClip recogerSonido;
    private AudioSource audioSource;

    private static bool yaRecogidoEnEsteFrame = false; // evita recoger varias carpetas al mismo tiempo

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (yaRecogidoEnEsteFrame) return; // otra carpeta ya se recogió este frame

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            yaRecogidoEnEsteFrame = true; // bloqueamos otras carpetas este frame
            RecogerFolder();
        }
    }

    void LateUpdate()
    {
        yaRecogidoEnEsteFrame = false; // reset para el siguiente frame
    }

    public void RecogerFolder()
    {
        if (Inventory.instance != null)
            Inventory.instance.AddFolder();

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