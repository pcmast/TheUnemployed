using UnityEngine;

public class FolderInteract : MonoBehaviour
{
    public float interactionDistance = 13f;
    public AudioClip recogerSonido;
    private AudioSource audioSource;

    private bool recogido = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (recogido) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            RecogerFolder();
        }
    }

    public void RecogerFolder()
    {
        if (recogido) return;
        recogido = true;

        if (Inventory.instance != null)
            Inventory.instance.AddFolder();

        GetComponent<Collider>().enabled = false;

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