using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private PlayerWeaponHandler player;

    void Start()
    {
        player = GetComponentInParent<PlayerWeaponHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo funciona si el jugador está atacando
        if (!player.IsAttacking()) return;

        // Buscar NPCRagdoll en el objeto golpeado o en su padre
        NPCRagdoll npc = other.GetComponentInParent<NPCRagdoll>();

        if (npc != null)
        {
            npc.Die();
        }
    }
}