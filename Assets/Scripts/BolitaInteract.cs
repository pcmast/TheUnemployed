using UnityEngine;

public class BolitaInteract : MonoBehaviour
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
            RecogerBolita();
        }
    }

    public void RecogerBolita()
    {
        if (recogido) return;
        recogido = true;

        if (Inventory.instance != null)
            Inventory.instance.AddPaperBall();

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