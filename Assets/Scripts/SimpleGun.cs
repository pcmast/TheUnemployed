using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public float range = 50f;

    void Update()
    {
        // Debug: rayo visible siempre en Scene View
        Debug.DrawRay(transform.position, transform.forward * range, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        // Usamos transform.forward de la cámara
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("HIT: " + hit.collider.name);

            // Intentamos obtener NPCHealth en el objeto padre
            NPCHealth npc = hit.collider.GetComponentInParent<NPCHealth>();
            if (npc != null)
            {
                Debug.Log("NPC ENCONTRADO - MUERE");
                npc.TakeDamage(1);
            }
        }
        else
        {
            Debug.Log("NO HIT");
        }
    }
}