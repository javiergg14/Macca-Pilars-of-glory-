using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float mouseSensitivity = 100f; // Sensibilidad del rat�n

    [Header("Limits")]
    public Vector2 xRotationLimits = new Vector2(-90f, 90f); // L�mites de rotaci�n en el eje X
    public Vector2 yRotationLimits = new Vector2(-90f, 90f); // L�mites de rotaci�n en el eje Y

    private float xRotation = 0f; // Rotaci�n acumulada en X
    private float yRotation = 0f; // Rotaci�n acumulada en Y

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
        // Captura el movimiento del rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ajusta las rotaciones acumuladas
        xRotation -= mouseY;
        yRotation += mouseX;

        // Aplica los l�mites de rotaci�n
        xRotation = Mathf.Clamp(xRotation, xRotationLimits.x, xRotationLimits.y);
        yRotation = Mathf.Clamp(yRotation, yRotationLimits.x, yRotationLimits.y);

        // Aplica la rotaci�n al transform de la c�mara
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}