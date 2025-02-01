using UnityEngine;

public class ProjectionBlockMovement : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Velocidad de rotaci�n en grados por segundo

    private void Start()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

        // Configuraci�n del Rigidbody
        rb.isKinematic = true;
        rb.useGravity = false;

        // Rotaci�n inicial de 45 grados en el eje X
        transform.rotation = Quaternion.Euler(-45, 0, 0);
    }

    private void Update()
    {
        // Rotaci�n continua en el eje Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
