using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Punto donde se sostiene el arma")]
    public Transform weaponHolder;

    [Header("Distancia de recogida")]
    public float pickupDistance = 2f;

    [Header("Ataque")]
    public float attackMoveDistance = 0.5f;
    public float attackSpeed = 10f;

    private bool isAttacking = false;
    private GameObject currentWeapon;
    private Vector3 attackStartPos;
    private Vector3 attackTargetPos;

    void Update()
    {
        HandlePickup();
        HandleDrop();
    }
    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapon"), LayerMask.NameToLayer("Player"), true);
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentWeapon == null)
        {
            // Detectar armas cercanas
            Collider[] hits = Physics.OverlapSphere(transform.position, pickupDistance);
            float minDist = Mathf.Infinity;
            GameObject nearest = null;

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Weapon"))
                {
                    float dist = Vector3.Distance(transform.position, hit.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearest = hit.gameObject;
                    }
                }
            }

            if (nearest != null)
            {
                PickUpWeapon(nearest);
            }
        }
    }

    void PickUpWeapon(GameObject weapon)
    {
        currentWeapon = weapon;

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null) Destroy(rb);

        Collider col = currentWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        currentWeapon.transform.SetParent(weaponHolder);

        // Posición y rotación según tipo de arma
        if (currentWeapon.name.Contains("Hacha"))
        {
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.Euler(90f, -90f, 0f); // ajusta según tu modelo
        }
        else if (currentWeapon.name.Contains("Cuchillo"))
        {
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.Euler(0f, -90f, 140f); // ajusta según tu modelo
        }
    }

    void HandleDrop()
    {
        if (currentWeapon == null) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            // Quitar de la mano
            currentWeapon.transform.SetParent(null);

            // Asegurarse de que tenga collider activo
            Collider col = currentWeapon.GetComponent<Collider>();
            if (col != null) col.enabled = true;
            else col = currentWeapon.AddComponent<BoxCollider>();

            // Asegurarse de que tenga Rigidbody
            Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
            if (rb == null) rb = currentWeapon.AddComponent<Rigidbody>();
            rb.mass = 2f;
            rb.isKinematic = false;

            currentWeapon = null;
        }
    }
    void HandleAttack()
    {
        if (currentWeapon == null) return;

        Transform hitboxTransform = currentWeapon.transform.Find("Hitbox");
        Collider hitbox = null;

        if (hitboxTransform != null)
            hitbox = hitboxTransform.GetComponent<Collider>();

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;

            attackStartPos = currentWeapon.transform.localPosition;
            attackTargetPos = attackStartPos + Vector3.forward * attackMoveDistance;

            // Activar hitbox
            if (hitbox != null)
                hitbox.enabled = true;
        }

        if (isAttacking)
        {
            currentWeapon.transform.localPosition =
                Vector3.Lerp(currentWeapon.transform.localPosition,
                             attackTargetPos,
                             Time.deltaTime * attackSpeed);

            if (Vector3.Distance(currentWeapon.transform.localPosition, attackTargetPos) < 0.01f)
            {
                currentWeapon.transform.localPosition =
                    Vector3.Lerp(currentWeapon.transform.localPosition,
                                 attackStartPos,
                                 Time.deltaTime * attackSpeed);

                if (Vector3.Distance(currentWeapon.transform.localPosition, attackStartPos) < 0.01f)
                {
                    currentWeapon.transform.localPosition = attackStartPos;
                    isAttacking = false;

                    // Desactivar hitbox al terminar ataque
                    if (hitbox != null)
                        hitbox.enabled = false;
                }
            }
        }
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }
}