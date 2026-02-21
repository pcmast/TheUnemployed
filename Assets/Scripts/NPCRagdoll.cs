using UnityEngine;
using System.Collections.Generic;
public class NPCRagdoll : MonoBehaviour
{
    private Rigidbody[] ragdollRigidbodies;
    private List<Collider> ragdollColliders = new List<Collider>();
    private Animator animator;

    // Collider principal que detecta disparos
    private Collider mainCollider;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Guardamos el collider principal (el que usamos para disparos)
        mainCollider = GetComponent<Collider>();

        // Cogemos solo los rigidbodies hijos (ragdoll)
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        // De todos los rigidbodies, guardamos solo los colliders asociados
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            Collider c = rb.GetComponent<Collider>();
            if (c != null)
            {
                // Excluimos el collider principal
                if (c != mainCollider)
                    ragdollColliders.Add(c);
            }
        }

        // Inicialmente desactivamos el ragdoll
        SetRagdoll(false);
    }

    void SetRagdoll(bool active)
    {
        // Animator activado solo mientras est� vivo
        animator.enabled = !active;

        // Colliders del ragdoll y rigidbodies
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            // Hacemos kinematic mientras est� vivo
            rb.isKinematic = !active;
        }

        foreach (Collider col in ragdollColliders)
        {
            // Solo activamos colliders de ragdoll al morir
            col.enabled = active;
        }

        // Collider principal siempre activo mientras est� vivo
        if (!active && mainCollider != null)
            mainCollider.enabled = true;
    }

    public void Die()
    {
        // Desactivamos animator, activamos ragdoll
        SetRagdoll(true);

        // Empujar un poco el cuerpo para que se vea m�s real
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.AddForce(Vector3.forward * 2f, ForceMode.Impulse);
        }
    }

    // Tecla de prueba
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }
}
