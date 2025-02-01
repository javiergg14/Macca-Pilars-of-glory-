using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject grab;

    public Camera projectionBlockCamera;

    public GameObject[] shapePrefabs;

    public Material ProjectBlockMaterial; // Material para el prefab "project"

    public float spawnHeightOffset = 0.5f;
    public float spawnDelay = 1.0f;

    // Bandera para controlar si se puede generar un nuevo objeto
    private bool canSpawn = true;

    void Start()
    {
        SpawnObjects();
    }

    // Función pública para generar objetos, puede ser llamada desde otras clases
    public void TriggerSpawn()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnWithDelay());
        }
    }

    void SpawnObjects()
    {
        // Asegúrate de que hay formas para generar, un grab y una cámara
        if (shapePrefabs.Length == 0 || grab == null || projectionBlockCamera == null)
        {
            Debug.LogWarning("Faltan formas, el cubo base o la cámara para generar objetos.");
            return;
        }

        // Seleccionar un prefab aleatorio para ambos objetos
        GameObject selectedPrefab = shapePrefabs[Random.Range(0, shapePrefabs.Length)];

        // Generar objeto debajo del grab
        Vector3 spawnPositionBelowGrab = grab.transform.position;
        spawnPositionBelowGrab.y -= grab.transform.localScale.y / 2 + spawnHeightOffset;

        GameObject newObjectBelowGrab = Instantiate(selectedPrefab, spawnPositionBelowGrab, Quaternion.identity);
        newObjectBelowGrab.layer = LayerMask.NameToLayer("Block");
        newObjectBelowGrab.AddComponent<ProjectionBlockMovement>();


        // Generar objeto en la proyección
        Vector3 spawnPositionInProjection = projectionBlockCamera.transform.position;
        spawnPositionInProjection += projectionBlockCamera.transform.forward * 5.0f;

        GameObject newObjectInProjection = Instantiate(selectedPrefab, spawnPositionInProjection, Quaternion.identity);
        newObjectInProjection.layer = LayerMask.NameToLayer("ProjectionBlock");
        newObjectInProjection.AddComponent<ProjectionBlockMovement>();

        AssignMaterial(newObjectInProjection, ProjectBlockMaterial);
    }

    void AssignMaterial(GameObject obj, Material material)
    {
        // Busca el Renderer del objeto y asigna el material
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogWarning($"El objeto {obj.name} no tiene un componente Renderer.");
        }
    }

    IEnumerator SpawnWithDelay()
    {
        // Espera el tiempo configurado antes de generar los siguientes objetos
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        SpawnObjects();
        canSpawn = true;
    }
}
