using UnityEngine;

public class ProjectionBlockMovement : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Velocidad de rotación en grados por segundo

    private void Start()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

        // Configuración del Rigidbody
        rb.isKinematic = true;
        rb.useGravity = false;

        // Rotación inicial de 45 grados en el eje X
        transform.rotation = Quaternion.Euler(-45, 0, 0);
    }

    private void Update()
    {
        // Rotación continua en el eje Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
