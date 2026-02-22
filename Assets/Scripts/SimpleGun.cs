using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public float range = 50f;
    public bool isEquipped = false;

    void Update()
    {
        // Debug: rayo visible siempre en Scene View
        Debug.DrawRay(transform.position, transform.forward * range, Color.red);

        if (!isEquipped) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Transform cam = Camera.main.transform;

        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            Debug.Log("HIT: " + hit.collider.name + " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            NPCHealth npc = hit.collider.GetComponentInParent<NPCHealth>();
            if (npc != null)
            {
                Debug.Log("NPC ENCONTRADO - MUERE");
                npc.TakeDamage(1);
            }
            else
            {
                Debug.Log("NO tiene NPCHealth");
            }
        }
        else
        {
            Debug.Log("NO HIT");
        }
    }
}

