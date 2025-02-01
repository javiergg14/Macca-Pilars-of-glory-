using UnityEngine;

public class BlockProperties : MonoBehaviour
{
    public bool hasCollided = false; // Marca si el bloque ya colisionó

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Platform")) && !hasCollided)
        {
            hasCollided = true;

            // Sumar puntos al colocar un bloque
            ScoreManager.instance.AddScore(10);

            // Encuentra el script DownPlane para bajar la plataforma
            DownPlane downPlane = FindObjectOfType<DownPlane>();
            if (downPlane != null)
            {
                downPlane.StartCoroutine(downPlane.MovePlatform());
            }
        }
    }

}
