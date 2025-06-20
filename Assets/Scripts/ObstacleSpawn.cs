using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] float[] lanes = { -2.5f, 2.5f };


    private Transform globalObstacleParent;
    private Coroutine spawnRoutine;
    private Transform playerTransform;

    private void Start()
    {
        GameObject found = GameObject.Find("ActiveObstacles");
        if (found != null)
            globalObstacleParent = found.transform;
        else
            Debug.LogWarning("ActiveObstacles GameObject not found in the scene!");

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;

        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawnRoutine == null)
            spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs.Count == 0) return;

        GameObject selectedObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];


        float laneX = lanes[Random.Range(0, lanes.Length)];
        Vector3 spawnPosition = new Vector3(laneX, transform.position.y + 3f, transform.position.z);


        GameObject obj = Instantiate(selectedObstacle, spawnPosition, selectedObstacle.transform.rotation, globalObstacleParent);

        Obstacle obstacle = obj.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            Vector3 launchDirection = -Vector3.forward;
            obstacle.Init(launchDirection);
        }
    }
}