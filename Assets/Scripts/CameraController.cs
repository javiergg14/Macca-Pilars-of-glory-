using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float mouseSensitivity = 100f; // Sensibilidad del ratón

    [Header("Limits")]
    public Vector2 xRotationLimits = new Vector2(-90f, 90f); // Límites de rotación en el eje X
    public Vector2 yRotationLimits = new Vector2(-90f, 90f); // Límites de rotación en el eje Y

    private float xRotation = 0f; // Rotación acumulada en X
    private float yRotation = 0f; // Rotación acumulada en Y

    void Start()
    {
        // Bloquea el cursor al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseRotation();
    }

    void HandleMouseRotation()
    {
        // Captura el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ajusta las rotaciones acumuladas
        xRotation -= mouseY;
        yRotation += mouseX;

        // Aplica los límites de rotación
        xRotation = Mathf.Clamp(xRotation, xRotationLimits.x, xRotationLimits.y);
        yRotation = Mathf.Clamp(yRotation, yRotationLimits.x, yRotationLimits.y);

        // Aplica la rotación al transform de la cámara
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}