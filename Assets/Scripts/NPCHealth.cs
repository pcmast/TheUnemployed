using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int health = 1;

    private NPCRagdoll ragdoll;

    void Start()
    {
        ragdoll = GetComponent<NPCRagdoll>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            ragdoll.Die();
            Destroy(this); // ya no necesita vida
        }
    }
}
