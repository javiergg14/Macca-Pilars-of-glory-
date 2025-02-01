using System.Collections;
using UnityEngine;


public class BlockFallDetector : MonoBehaviour
{
    public DownPlane downPlane; // Referencia al script que controla la plataforma
    public AudioSource audioSource; // Referencia al AudioSource
    public float soundCooldown = 1.0f; // Tiempo de cooldown entre sonidos
    private bool canPlaySound = true; // Controla si el sonido puede reproducirse

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log($"Bloque {other.gameObject.name} ha caído fuera de la plataforma.");

            // Restar puntos al caer un bloque
            ScoreManager.instance.SubtractScore(5);

            // Destruir el bloque
            Destroy(other.gameObject);

            // Subir la plataforma si corresponde
            if (downPlane != null)
            {
                downPlane.StartCoroutine(downPlane.RaisePlatform());
            }

            // Reproducir sonido si el cooldown lo permite
            if (canPlaySound && audioSource != null)
            {
                audioSource.Play();
                StartCoroutine(SoundCooldown());
            }
        }
    }

    private IEnumerator SoundCooldown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(soundCooldown);
        canPlaySound = true;
    }
}
