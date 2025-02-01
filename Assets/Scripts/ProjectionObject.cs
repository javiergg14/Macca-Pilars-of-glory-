using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RaycastSilhouette : MonoBehaviour
{
    public Transform objectToProject; // El objeto que proyectará la sombra
    public Material shadowMaterial;  // Material para la sombra (transparente recomendado)
    public LayerMask groundLayer;    // Capa(s) que representan las superficies que recibirán la sombra
    public float shadowOffset = 0.01f; // Altura de la sombra sobre la superficie para evitar z-fighting

    private GameObject shadowObject; // Objeto que representa la sombra

    void Start()
    {
        // Crear un Quad para la sombra
        shadowObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        shadowObject.name = "ShadowProjection";
        shadowObject.GetComponent<MeshRenderer>().material = shadowMaterial;
        shadowObject.transform.localScale = Vector3.one; // Tamaño inicial
        Destroy(shadowObject.GetComponent<Collider>()); // Eliminar el collider del Quad
    }

    void Update()
    {
        if (objectToProject == null || shadowObject == null)
            return;

        // Realizar un Raycast desde el objeto hacia abajo
        Ray ray = new Ray(objectToProject.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            // Posicionar la sombra en el eje Y del plano detectado
            shadowObject.transform.position = new Vector3(objectToProject.position.x, hitInfo.point.y + shadowOffset, objectToProject.position.z);

            // Ajustar la rotación de la sombra según la orientación del objeto proyectado
            Quaternion objectRotation = objectToProject.rotation;
            shadowObject.transform.rotation = Quaternion.Euler(90, objectRotation.eulerAngles.y, 0);

            // Escalar la sombra en función del tamaño del objeto proyectado
            Renderer objectRenderer = objectToProject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                Bounds objectBounds = objectRenderer.bounds;
                shadowObject.transform.localScale = new Vector3(objectBounds.size.x, objectBounds.size.z, 1);
            }

            // Asegurarse de que la sombra esté visible
            shadowObject.SetActive(true);
        }
        else
        {
            // Ocultar la sombra si no se encuentra ninguna superficie debajo
            shadowObject.SetActive(false);
        }
    }
}
