using UnityEngine;

public class EyesLookAtPlayer : MonoBehaviour
{
    [Header("Objetivo a mirar")]
    public Transform target; // Cámara del jugador

    [Header("Ojos a rotar")]
    public Transform leftEye;
    public Transform rightEye;

    [Header("Opciones")]
    public float rotationSpeed = 5f;   // Suavidad de la rotación
    public float maxAngle = 45f;       // Máximo ángulo de giro desde la posición inicial

    private Quaternion leftInitialRotation;
    private Quaternion rightInitialRotation;

    void Start()
    {
        if (leftEye != null) leftInitialRotation = leftEye.localRotation;
        if (rightEye != null) rightInitialRotation = rightEye.localRotation;

        // Si no se asigna target, usamos la cámara principal
        if (target == null && Camera.main != null)
        {
            target = Camera.main.transform;
        }
    }

    // LateUpdate para que se ejecute después del Animator
    void LateUpdate()
    {
        if (target == null || leftEye == null || rightEye == null) return;

        RotateEye(leftEye, leftInitialRotation);
        RotateEye(rightEye, rightInitialRotation);
    }

    void RotateEye(Transform eye, Quaternion initialRotation)
    {
        // Dirección desde el ojo hacia el objetivo
        Vector3 direction = (target.position - eye.position).normalized;

        // Rotación deseada usando LookRotation
        Quaternion lookRotation = Quaternion.LookRotation(direction, eye.up);

        // Aplicamos suavidad
        eye.rotation = Quaternion.Slerp(eye.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Convertimos a local para poder limitar el giro
        Vector3 localEuler = eye.localEulerAngles;
        localEuler.x = ClampAngle(localEuler.x, -maxAngle, maxAngle);
        localEuler.y = ClampAngle(localEuler.y, -maxAngle, maxAngle);
        localEuler.z = 0f; // Siempre mantenemos roll cero
        eye.localEulerAngles = localEuler;
    }

    float ClampAngle(float angle, float min, float max)
    {
        // Convertimos de 0-360 a -180-180
        angle = (angle > 180) ? angle - 360 : angle;
        return Mathf.Clamp(angle, min, max);
    }
}