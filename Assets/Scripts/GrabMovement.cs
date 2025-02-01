using UnityEngine;

public class HookController : MonoBehaviour
{
    [Header("Movement Area Limits")]
    public float leftLimit = -5f;
    public float rightLimit = 5f;
    public float topLimit = 5f;
    public float bottomLimit = -5f;

    [Header("Movement Speed")]
    public float speed = 5f;

    [Header("Movement Sound")]
    public AudioClip movementSound; // Clip de sonido para el movimiento
    private AudioSource _audioSource;

    private Vector3 _newPosition;

    void Start()
    {
        // Configura el AudioSource
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = movementSound;
        _audioSource.loop = false; // No queremos que el sonido se repita automáticamente
    }

    void Update()
    {
        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrows
        float verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down Arrows

        // Calculate new position
        _newPosition = transform.position;
        _newPosition.x += horizontalInput * speed * Time.deltaTime;
        _newPosition.z += verticalInput * speed * Time.deltaTime;

        // Clamp position within limits
        _newPosition.x = Mathf.Clamp(_newPosition.x, leftLimit, rightLimit);
        _newPosition.z = Mathf.Clamp(_newPosition.z, bottomLimit, topLimit);

        // Apply the new position
        transform.position = _newPosition;

        // Play sound if there is movement
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }

    // Draw the limits in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, 0, bottomLimit), new Vector3(rightLimit, 0, bottomLimit));
        Gizmos.DrawLine(new Vector3(rightLimit, 0, bottomLimit), new Vector3(rightLimit, 0, topLimit));
        Gizmos.DrawLine(new Vector3(rightLimit, 0, topLimit), new Vector3(leftLimit, 0, topLimit));
        Gizmos.DrawLine(new Vector3(leftLimit, 0, topLimit), new Vector3(leftLimit, 0, bottomLimit));
    }
}
