using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    /// <summary>
    /// Spawn per/second
    /// </summary>
    public float spawnRate = 1f;
    public float radius = 25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        StartCoroutine(Spawn(spawnPrefab));
    }

    public void CreateObjectAtPoint(GameObject prefab, Vector3 point)
    {
        Instantiate(prefab, point, transform.rotation);
    }

    public IEnumerator Spawn(GameObject prefab)
    {
        yield return new WaitForSeconds(1 / spawnRate);
        Debug.Log("Spawning");
        Vector3 point = GetRandomPointOnTerrain();
        CreateObjectAtPoint(prefab, point);
        StartCoroutine(Spawn(spawnPrefab));
    }

    public Vector3 GetRandomPointOnTerrain()
    {
        Vector3 randomPoint = transform.position +Random.insideUnitSphere * radius;
        randomPoint.y = Terrain.activeTerrain.SampleHeight(randomPoint);
        return randomPoint;
    }
}

