using System.Collections;
using UnityEngine;

public class DownPlane : MonoBehaviour
{
    public float dropDistance = 0.5f; // Distancia que baja o sube la plataforma
    public float dropSpeed = 2f; // Velocidad de movimiento
    public Transform sensor; // Referencia al sensor, para moverlo junto con la plataforma

    private Vector3 targetPosition; // Posición objetivo de la plataforma

    void Start()
    {
        // Establece la posición inicial como objetivo inicial
        targetPosition = transform.position;
    }

    public IEnumerator MovePlatform()
    {
        // Calcula la nueva posición objetivo para bajar
        targetPosition -= new Vector3(0, dropDistance, 0);

        // Mueve la plataforma hacia abajo
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dropSpeed);

            // Mueve también el sensor
            if (sensor != null)
            {
                sensor.position = new Vector3(sensor.position.x, transform.position.y - 1f, sensor.position.z);
            }

            yield return null;
        }

        transform.position = targetPosition;
    }

    public IEnumerator RaisePlatform()
    {
        // Calcula la nueva posición objetivo para subir
        targetPosition += new Vector3(0, dropDistance, 0);

        // Mueve la plataforma hacia arriba
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dropSpeed);

            // Mueve también el sensor
            if (sensor != null)
            {
                sensor.position = new Vector3(sensor.position.x, transform.position.y - 1f, sensor.position.z);
            }

            yield return null;
        }

        transform.position = targetPosition;
    }
}
